using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int currentSceneIndex;

    // Use this for initialization
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnStartGame() {

        GameManagerScript.GameManager.UiManagerScripto.IntroScene.SetActive(true);
        GameManagerScript.GameManager.UiManagerScripto.OnPlayingUI(true);
        SceneManager.LoadScene(2);
   

    }


    public void ExitGame() {
        Application.Quit();
    }
}
