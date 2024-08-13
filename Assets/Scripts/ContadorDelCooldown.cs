using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorDelCooldown : MonoBehaviour
{
    Jugador jugador;
    public TextMeshProUGUI textoTiempo;
    float cooldown;

    private void Awake() {
        textoTiempo = GetComponent<TextMeshProUGUI>();
        jugador = FindObjectOfType<Jugador>();
    }
    void Update()
    {
        if(gameObject.name == "CooldownDisparoRapido") {
            cooldown = jugador.cooldownDeDisparoRapido;
        }
        else if (gameObject.name == "CooldownRelentizacion") {
            cooldown = jugador.cooldownDeRelentizacion;
        }
        //Debug.Log(cooldown);

        if(cooldown >= 0) {
            textoTiempo.text = "" + (int)cooldown;
        }
        else {
            textoTiempo.text = "0";
        }

    }
}
