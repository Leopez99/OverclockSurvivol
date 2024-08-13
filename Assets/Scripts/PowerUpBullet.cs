using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBullet : MonoBehaviour
{
    float tiempoDePowerUp;

    private void Awake() {
        tiempoDePowerUp = 10;
    }

    private void OnTriggerEnter(Collider other) {
        IGolpeable otroObjeto = other.gameObject.GetComponent<IGolpeable>();

        if (otroObjeto != null && other.tag == "Player") {
            GameManager.INS.ActivoPowerUpBullet = true;
            GameManager.INS.cooldownDeBullet += tiempoDePowerUp;
            Destroy(gameObject);
        }
    }
}
