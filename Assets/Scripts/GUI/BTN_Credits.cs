using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BTN_Credits : MonoBehaviour
{
    private Button boton;

    private void Awake() {
        boton = GetComponent<Button>();
    }

    private void Start() {
        boton.onClick.AddListener(MenuPrincipal);
    }

    private void MenuPrincipal() {
        SceneManager.LoadScene("Credits");
    }
}
