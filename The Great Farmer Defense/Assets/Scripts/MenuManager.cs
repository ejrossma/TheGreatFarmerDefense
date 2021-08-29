using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject optionsCanvas;
    public GameObject visualTutorialCanvas;
    public GameObject visualTutorialCanvas2;

    void Start() {
        visualTutorialCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        visualTutorialCanvas2.SetActive(false);
    }

    public void start() {
        visualTutorialCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    public void options() {
        menuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void exit() {
        Application.Quit();
    }

    public void returnMenu() {
        menuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

    public void continueToGame() {
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
    }

    public void page2() {
        visualTutorialCanvas.SetActive(false);
        visualTutorialCanvas2.SetActive(true);
    }


}
