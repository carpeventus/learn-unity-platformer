using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject loadingScreen;
    public TMP_Text progressText;
    public void StartGame() {
        StartCoroutine(AsyncLoad());
    }

    public IEnumerator AsyncLoad() {
        loadingScreen.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!asyncOperation.isDone) {
            var progress = asyncOperation.progress / 0.9f;
            progressText.text = "Loading..." + progress * 100f + "%";
            yield return null;
        }
    }
    
    public void QuitGame() {
        Application.Quit();
    } 
}
