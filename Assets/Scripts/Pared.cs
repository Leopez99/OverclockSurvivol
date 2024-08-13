using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pared : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision) {
        IGolpeable otroObjeto = collision.gameObject.GetComponent<IGolpeable>();

        if (otroObjeto != null) {
            otroObjeto.Eliminarse();
        }

        if (collision.gameObject.tag == "Bala") {
            collision.GetComponent<Bala>().Eliminarse();
        }
    }
}
