using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject startMenu;

    private void Start() {
        startMenu.SetActive(false);
    }

    public void StartButton() {
        startMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }

    public void LoadLevel(int sceneNum) {
        SceneManager.LoadScene(sceneNum);
    }

    public void Return() {
        startMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
