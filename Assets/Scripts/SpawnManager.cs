using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabsEnemigos;
    public static SpawnManager spawnManagerINS;

    private void Awake() {
        spawnManagerINS = this;
    }

    void Start()
    {
        InvokeRepeating("InstanciarEnemigo", 1.5f, 1.5f);
    }

    private void InstanciarEnemigo() {
        Vector2 posicion = new Vector2(Random.Range(-55, 55), 27);
        Quaternion rotacion = Quaternion.Euler(0, 0, 0);
        Instantiate(prefabsEnemigos[Random.Range(0, prefabsEnemigos.Length)], posicion, rotacion);
    }

    public void PararSpawn() {
        CancelInvoke();
    }

    public void ContinuarSpawn() {
        InvokeRepeating("InstanciarEnemigo", 1.5f, 1.5f);
    }
}
