using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPowerUp : MonoBehaviour
{
    [SerializeField] GameObject[] powerUps;
    private int tiempoParaRespawnearObjeto;

    private void Awake() {
        tiempoParaRespawnearObjeto = 30;
    }

    private void Start() {
        Instantiate(powerUps[Random.Range(0, powerUps.Length)], transform);    
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<Jugador>() != null) {
            Invoke("ReiniciarSpawneoAleatorio", tiempoParaRespawnearObjeto);
        }
    }

    private void ReiniciarSpawneoAleatorio() {
        Vector2 posicion = new Vector2(Random.Range(transform.position.x + 5, transform.position.x - 5), Random.Range(transform.position.y + 5, transform.position.y - 5));
        Quaternion rotacion = Quaternion.Euler(0, 0, 0);
        Instantiate(powerUps[Random.Range(0, powerUps.Length)], posicion, rotacion);
    }
}
