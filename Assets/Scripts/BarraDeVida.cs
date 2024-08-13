using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVida : MonoBehaviour {
    public SpriteRenderer barraDeVidaSprite; // El SpriteRenderer que actuará como la barra de vida
    public float vidaMaxima; // Vida máxima
    private float vidaActual;

    private void Awake() {
        barraDeVidaSprite = GetComponent<SpriteRenderer>();
    }

    void Start() {
        vidaMaxima = GetComponentInParent<Jugador>().vida;
        vidaActual = vidaMaxima; // Inicializar la vida al máximo
    }

    public void RecibirDaño(float cantidad) {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima); // Asegurarse de que la vida no baje de 0
        ActualizarBarraDeVida();
    }

    void ActualizarBarraDeVida() {
        // Calcular la escala en el eje X basada en la vida actual
        float escalaX = vidaActual / vidaMaxima;
        barraDeVidaSprite.transform.localScale = new Vector3(escalaX, 0.5f, 1f);
    }
}
