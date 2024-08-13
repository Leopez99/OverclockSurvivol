using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

    [SerializeField] float velocidad;
    public int da�o;
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
            da�o = 1;
        }
        else {
            da�o = 5;
        }

        if (otroObjeto != null && other.tag != "Player") {
            otroObjeto.RestarVida(da�o);

            if(other.GetComponent<FlashDarker>() != null)
                other.gameObject.GetComponent<FlashDarker>().TriggerDarkenEffect();

            if(!GameManager.INS.ActivoPowerUpBullet || other.tag == "Pared")
                Eliminarse();
        }
    }


    public void RestarVida(int da�o) {
        //Metodo vacio
    }
}
