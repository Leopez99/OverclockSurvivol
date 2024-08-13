using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour, IGolpeable
{
    [SerializeField] private float velocidadDeMovimiento;
    private int tiempoDeAceleracionDeDisparo;
    public int vida;
    //private Animator animator;
    [SerializeField] private Vector2 input;
    private PlayerInput playerInput;
    private Arma arma;
    private Animator animator;
    //Habilidades del jugador
    public float cooldownDeRelentizacion;
    private bool puedoUsarRelentizacion;
    public float cooldownDeDisparoRapido;
    private bool puedoUsarDisparoRapido;
    //Power ups
    public Animator animatorDisparoRapido;
    public Animator animatorRelentizacion;
    [SerializeField] AudioSource speedAudio;
    [SerializeField] AudioSource slowAudio;
    // Límites del mapa (área cuadrada)
    [SerializeField] private Vector2 minBounds; // Coordenadas mínimas (esquina inferior izquierda)
    [SerializeField] private Vector2 maxBounds; // Coordenadas máximas (esquina superior derecha)

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        arma = GetComponentInChildren<Arma>();
        animator = GetComponent<Animator>();
        vida = 100;
        tiempoDeAceleracionDeDisparo = 3;
        
    }


    private void Update() {
        Moverse();

        // Cerra juego
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MenuPrincipal");
        }

        if (cooldownDeRelentizacion < 1) {
            puedoUsarRelentizacion = true;
        }
        else {
            puedoUsarRelentizacion = false;
        }

        cooldownDeRelentizacion -= Time.deltaTime;        
        
        if(cooldownDeDisparoRapido < 1) {
            puedoUsarDisparoRapido = true;
        }
        else {
            puedoUsarDisparoRapido = false;
        }

        cooldownDeDisparoRapido -= Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.Escape)) {
        //    Application.Quit();
        //    //EditorApplication.isPlaying = false;
        //}
    }

    public void Moverse() {
        input = playerInput.actions["Movimiento"].ReadValue<Vector2>();
        Vector3 newPosition = transform.position + new Vector3(input.x, input.y, 0) * velocidadDeMovimiento * Time.deltaTime;

        // Limitar la posición dentro de los límites del cuadrado
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        transform.position = newPosition;

        if (input.x != 0 || input.y != 0) {
            animator.SetBool("SeMueve", true);
        }
        else {
            animator.SetBool("SeMueve", false);
        }
    }

    public void AceleracionDeAtaque(InputAction.CallbackContext callbackContext) {
        if (callbackContext.performed && puedoUsarDisparoRapido) {
            speedAudio.Play();
            animatorDisparoRapido.Play("DisparoRapido");
            StartCoroutine(Aceleracion());
            cooldownDeDisparoRapido = 15;
        }
    }

    public void Dash(InputAction.CallbackContext callbackContext) {
        if (callbackContext.performed) {
            StartCoroutine(RealizarDash());
        }
    }

    public void RelentizarTiempo(InputAction.CallbackContext callbackContext) {
        if (callbackContext.performed && puedoUsarRelentizacion) {
            slowAudio.Play();
            //Debug.Log("Relentice el tiempo");
            RelentizacionDeEnemigos();
            animatorRelentizacion.Play("Relentizacion");
            cooldownDeRelentizacion = 20; //Vuelve a empezar el cooldown
        }
    }

    private void RelentizacionDeEnemigos() {
        Enemigo[] enemigos = FindObjectsOfType<Enemigo>();
        SpawnManager.spawnManagerINS.PararSpawn();
        foreach (var enemigoActual in enemigos) {
            Debug.Log(enemigoActual.gameObject.name);
            enemigoActual.tiempoRelentizado = true;
        }
        Invoke("DesactivarRelentizacion", 5);
    }

    private void DesactivarRelentizacion() {
        Enemigo[] enemigos = FindObjectsOfType<Enemigo>();

        foreach (var enemigoActual in enemigos) {
            enemigoActual.tiempoRelentizado = false;
        }
        SpawnManager.spawnManagerINS.ContinuarSpawn();
    }

    IEnumerator Aceleracion() {
        arma.cadenciaDeDisparo = arma.actualizacionDeCadencia;
        yield return new WaitForSeconds(tiempoDeAceleracionDeDisparo);
        DesactivarAceleracion();
    }

    private void DesactivarAceleracion() {
        arma.cadenciaDeDisparo = arma.cadenciaDeDisparoNormal;
    }

    private IEnumerator RealizarDash() {
        Vector3 start = transform.position;
        Vector3 dashDirection = new Vector3(input.x, input.y, 0).normalized;
        Vector3 end = start + dashDirection * 5f; // Cambia 5f por la distancia deseada del dash
        float dashDuration = 0.2f; // Cambia 0.2f por la duración del dash
        float elapsed = 0f;

        // Configuración de la capa para el raycast (por ejemplo, ignorar la capa 8)
        int layerMask = ~LayerMask.GetMask("LayerToIgnore"); // Ajusta "LayerToIgnore" al nombre de la capa que quieres ignorar

        RaycastHit hit;
        //Debug.Log(Physics.Raycast(start, dashDirection, out hit, 5f));
        // Realizar el raycast
        if (Physics.Raycast(start, dashDirection, out hit, 5f)) {
            // Si el raycast golpea algo, ajusta la distancia del dash al punto de impacto
            Debug.DrawRay(start, dashDirection * hit.distance, Color.yellow, 1f);
            end = hit.point; // Ajusta el punto final al punto de impacto
        }
        else {
            // Si el raycast no golpea nada, dibuja el raycast completo
            Debug.DrawRay(start, dashDirection * 5f, Color.white, 1f);
        }

        while (elapsed < dashDuration) {
            transform.position = Vector3.Lerp(start, end, elapsed / dashDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end; // Asegurarse de que el personaje llega a la posición final
    }

    public void Eliminarse() {
        //Metodo vacio
    }

    public void RestarVida(int daño) {
        vida -= daño;
        GetComponentInChildren<BarraDeVida>().RecibirDaño(daño);
    }
}

