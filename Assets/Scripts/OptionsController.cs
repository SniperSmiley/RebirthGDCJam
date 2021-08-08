using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] float defaultVolume;
    public Toggle FullScreenToggle;
 

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = defaultVolume;
        musicSlider.value = PlayerPrefsController.GetMusicVolume();


    }

    // Update is called once per frame
    void Update()
    {
        var musicPlayer = FindObjectOfType<AudioManager>();
        if(musicPlayer)
        {
            musicPlayer.SetVolume(musicSlider.value);
        }
        else
        {
            Debug.LogWarning("No Music Player");
        }
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("Fullscreen is" + isFullScreen);

    }

    public void SaveAndExit()
    {
        PlayerPrefsController.SetMusicVolume(musicSlider.value);
        FindObjectOfType<LevelLoader>().LoadMainMenuScene();
    }

    public void Defaults()
    {
        musicSlider.value = defaultVolume;
    }
}
