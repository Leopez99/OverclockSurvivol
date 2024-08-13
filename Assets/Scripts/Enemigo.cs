using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour, IGolpeable
{
    private Jugador jugador;
    private Transform objetivo;      // El objeto al que se quiere seguir
    public bool tiempoRelentizado;
    [SerializeField] float velocidad = 5f;    // La velocidad de persecuci�n
    [SerializeField] int vida;
    [SerializeField] int da�o;
    [SerializeField] float intervaloEntreDa�o; // Cada cuantos segundos hace da�o
    private Animator animator;
    private float contadorTiempo = 0f; // Acumula el tiempo transcurrido desde que se empieza a hacer da�o
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        jugador = FindAnyObjectByType<Jugador>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        SeguirAlJugador();
    }


    private void SeguirAlJugador() {

        if (!tiempoRelentizado) {
            velocidad = VelocidadActualizada();
            animator.SetBool("Relentizado", false);
        }
        else {
            velocidad = 0.8f;
            animator.SetBool("Relentizado", true);
        }

        // Direcci�n desde el perseguidor hacia el objetivo
        objetivo = jugador.transform;
        Vector3 direccion = (objetivo.position - transform.position).normalized;

        // Mover el objeto hacia el objetivo
        transform.position += direccion * velocidad * Time.deltaTime;

        // Voltear el sprite seg�n la direcci�n de movimiento en el eje X
        if (direccion.x > 0) {
            spriteRenderer.flipX = false;  // No voltear el sprite
        }
        else if (direccion.x < 0) {
            spriteRenderer.flipX = true;  // Voltear el sprite
        }

    }

    private float VelocidadActualizada() {

        float tiempoActual = GameManager.INS.contadorDeTiempo;

        if (tiempoActual >= 240 && tiempoActual <= 300) {
            return 5f;
        }
        else if (tiempoActual >= 180 && tiempoActual <= 240) {
            return 6f;
        }
        else if (tiempoActual >= 120 && tiempoActual <= 180) {
            return 7f;
        }
        else if (tiempoActual >= 60 && tiempoActual <= 120) {
            return 8f;
        }
        else {
            return 8.6f;
        }
    }

    private void OnCollisionStay(Collision collision) {
        IGolpeable objetoGolpeado = collision.gameObject.GetComponent<IGolpeable>();


        // Acumular el tiempo transcurrido

        if (objetoGolpeado != null && collision.gameObject.tag == "Player") {
            contadorTiempo += Time.deltaTime;

            // Verificar si ha pasado suficiente tiempo para ejecutar la acci�n
            if (contadorTiempo >= intervaloEntreDa�o) {
                // Ejecutar la acci�n
                objetoGolpeado.RestarVida(da�o);
                //AccionRepetitiva();

                // Restablecer el contador, teniendo en cuenta el excedente
                contadorTiempo = 0f;
            }
        }
    }
    void AccionRepetitiva() {
        // Aqu� se coloca la acci�n que quieres repetir
        Debug.Log("Acci�n repetida cada " + intervaloEntreDa�o + " segundos");
    }
    public void RestarVida(int da�o) {
        vida -= da�o;
        if (vida <= 0)
            Eliminarse();
    }

    public void Eliminarse() {
        Destroy(gameObject);
    }

    private void MovimientoRelentizado() {
        velocidad = 2f;
    }
}
