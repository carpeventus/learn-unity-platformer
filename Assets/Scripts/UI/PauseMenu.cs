using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseUI;

    public void InputPause() {
        Pause();
    }

    public void Resume() {
        GameController.isGamePause = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void MainMenu() {
        Time.timeScale = 0.0f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void Pause() {
        GameController.isGamePause = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0.0f;
    }
    
    public void QuitGame() {
        Application.Quit();
    }
}
