using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource audioSource;  // Asigna el AudioSource desde el inspector
    public float volumeStep = 0.1f;  // Incremento/decremento de volumen
    public float minVolume = 0f;     // Volumen mínimo
    public float maxVolume = 1f;     // Volumen máximo
    private bool isMuted = false;    // Estado de mute

    void Update() {
        // Incrementar volumen con la tecla "+"
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus)) {
            ChangeVolume(volumeStep);
        }

        // Decrementar volumen con la tecla "-"
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) {
            ChangeVolume(-volumeStep);
        }

        // Mute/Desmute con la tecla "M"
        if (Input.GetKeyDown(KeyCode.M)) {
            ToggleMute();
        }
    }

    void ChangeVolume(float change) {
        if (!isMuted) {
            // Ajustar el volumen dentro de los límites establecidos
            audioSource.volume = Mathf.Clamp(audioSource.volume + change, minVolume, maxVolume);
        }
    }

    void ToggleMute() {
        isMuted = !isMuted; // Cambia el estado de mute
        audioSource.mute = isMuted; // Aplica el mute al AudioSource
    }
}
