using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager INS;
    public float contadorDeTiempo;
    [SerializeField] GameObject panelDerrota;
    [SerializeField] GameObject panelVictoria;
    public bool ActivoPowerUpBullet;
    public float cooldownDeBullet;
    public bool ActivoPowerDamage;
    public float cooldownDeDamage;
    private Jugador jugador;
    [SerializeField] GameObject iconoDamage;
    [SerializeField] GameObject iconoBullet;

    private void Awake() {
        Time.timeScale = 1;
        contadorDeTiempo = 300;
        //contadorDeTiempo = 10;
        INS = this;
        cooldownDeBullet = 0;
        cooldownDeDamage = 0;
        jugador = FindObjectOfType<Jugador>();
    }

    private void Update() {
        contadorDeTiempo -= Time.deltaTime;
        //Debug.Log(contadorDeTiempo);
        iconoBullet.SetActive(ActivoPowerUpBullet);
        if (ActivoPowerUpBullet) {
            cooldownDeBullet -= Time.deltaTime;
            if(cooldownDeBullet <= 0) {
                ActivoPowerUpBullet = false;
                cooldownDeBullet = 10;
            }
        }
        iconoDamage.SetActive(ActivoPowerDamage);
        if (ActivoPowerDamage) {
            cooldownDeDamage -= Time.deltaTime;
            if (cooldownDeDamage <= 0) {
                ActivoPowerDamage = false;
                cooldownDeDamage = 10;
            }
        }

        Ganar();
        Perder();

    }

    private void Ganar() {
        if(contadorDeTiempo <= 1) {
            Time.timeScale = 0;
            panelVictoria.SetActive(true);
        }
    }

    private void Perder() {
        if (jugador.vida <= 0) {
            Time.timeScale = 0;
            panelDerrota.SetActive(true);
        }
    }
}
