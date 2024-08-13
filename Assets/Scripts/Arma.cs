using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {
    public Transform character;                 // Referencia al transform del personaje
    public float distanceFromCharacter;         // Distancia entre el personaje y el arma
    public float cadenciaDeDisparoNormal;
    [SerializeField] GameObject bala;
    [SerializeField] GameObject puntaDelArma;
    [SerializeField] GameObject disparadorArriba;
    [SerializeField] GameObject disparadorAbajo;
    public float actualizacionDeCadencia;
    public float cadenciaDeDisparo;
    private SpriteRenderer spriteDelJugador;
    private SpriteRenderer spriteDelArma;

    private void Awake() {
        cadenciaDeDisparoNormal = 0.9f;
        actualizacionDeCadencia = 0.1f;
        cadenciaDeDisparo = cadenciaDeDisparoNormal;
        spriteDelArma = GetComponent<SpriteRenderer>();
        
    }

    private void Start() {
        DispararBalas();
        spriteDelJugador = character.GetComponent<SpriteRenderer>();
    }

    void Update() {
        RotarAlrededorDeJugador();
    }

    private void RotarAlrededorDeJugador() {

        // Obtener la posición del mouse en el mundo 2D
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calcular la dirección desde el personaje hasta el mouse
        Vector3 direction = mousePosition - character.position;
        direction.z = 0; // Asegurarse de que esté en el plano 2D

        // Calcular el ángulo de rotación
        float angle = Mathf.Atan2(direction.y, direction.x);

        // Calcular la nueva posición del arma
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distanceFromCharacter;
        transform.position = character.position + offset;

        // Calcular el ángulo en el que debe rotar el arma
        float angulo2 = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplicar la rotación al arma
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo2));

        //Rotacion del jugador
        if (mousePosition.x < character.position.x) {
            spriteDelJugador.flipX = true;
        }
        else {
            spriteDelJugador.flipX = false;
        }

        //Rotacion del arma
        if (mousePosition.x < character.position.x) {
            spriteDelArma.flipY = true;
            puntaDelArma = disparadorAbajo;
        }
        else {
            spriteDelArma.flipY = false;
            puntaDelArma = disparadorArriba;
        }
    }

    private void DispararBalas() {
        StartCoroutine(Disparos());
    }

    IEnumerator Disparos() {
        Quaternion rotacion = Quaternion.Euler(0, 0, 90);
        yield return new WaitForSeconds(cadenciaDeDisparo);
        Instantiate(bala, puntaDelArma.transform.position, puntaDelArma.transform.rotation);
        StartCoroutine(Disparos());
    }
}
