using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlashDarker : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRenderer;
    public float darkenDuration = 0.1f;  // Duración en segundos del efecto de oscurecimiento
    public Color darkenColor = new Color(0.5f, 0.5f, 0.5f, 1f); // Color más oscuro (puedes ajustar este valor)

    private Color originalColor;

    void Start() {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TriggerDarkenEffect() {
        StartCoroutine(DarkenAndReturn());
    }

    private IEnumerator DarkenAndReturn() {
        // Cambiar el color a un tono más oscuro
        spriteRenderer.color = darkenColor;

        // Esperar el tiempo especificado
        yield return new WaitForSeconds(darkenDuration);

        // Volver al color original
        spriteRenderer.color = originalColor;
    }
}

