using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tiempo : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;

    private void Awake() {
        textoTiempo = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        textoTiempo.text = "Time: " + (int)GameManager.INS.contadorDeTiempo;
    }
}
