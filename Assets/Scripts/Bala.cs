using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    [SerializeField] float velocidad;
    public int daño;
    private BoxCollider boxCollider;

    private void Awake() {
        boxCollider = GetComponent<BoxCollider>();
}

    private void Update() {
        transform.Translate(velocidad * Time.deltaTime, 0, 0);
    }

    public void Eliminarse() {
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        IGolpeable otroObjeto = other.gameObject.GetComponent<IGolpeable>();

        if (!GameManager.INS.ActivoPowerDamage) {
            daño = 1;
        }
        else {
            daño = 5;
        }

        if (otroObjeto != null && other.tag != "Player") {
            otroObjeto.RestarVida(daño);

            if(other.GetComponent<FlashDarker>() != null)
                other.gameObject.GetComponent<FlashDarker>().TriggerDarkenEffect();

            if(!GameManager.INS.ActivoPowerUpBullet || other.tag == "Pared")
                Eliminarse();
        }
    }


    public void RestarVida(int daño) {
        //Metodo vacio
    }
}
