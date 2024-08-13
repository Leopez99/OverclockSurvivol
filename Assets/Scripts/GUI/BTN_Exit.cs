using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BTN_Exit : MonoBehaviour
{
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
    }

    private void Start() {
        button.onClick.AddListener(() => Salir());
    }

    private void Salir() {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }
}
