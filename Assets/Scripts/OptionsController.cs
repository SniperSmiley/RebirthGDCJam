using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{

    public AudioMixer mixer;

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectsSlider;

    float defaultMusic;
    float defaultMaster;
    float defaultSFX;

    public Toggle FullScreenToggle;
 

    // Start is called before the first frame update
    void Start()
    {
        mixer.GetFloat("Volume", out defaultMaster);         MasterSlider.value = defaultMaster;
        mixer.GetFloat("MusicVolume", out defaultMusic);     MusicSlider.value = defaultMusic;
        mixer.GetFloat("EffectsVolume", out defaultSFX);     EffectsSlider.value = defaultSFX;


        /*
        musicSlider.value = defaultVolume;
        musicSlider.value = PlayerPrefsController.GetMusicVolume();
        */

    }

    public void SetMasterVolumeReal(float volume) { mixer.SetFloat("Volume", volume);  }

    public void SetMusicVolume(float volume) {mixer.SetFloat("MusicVolume", volume); }

    public void SetEffectsVolume(float volume) { mixer.SetFloat("EffectsVolume", volume); }


    // Update is called once per frame
    void Update()
    {
        /*
        var musicPlayer = FindObjectOfType<AudioManager>();
        if(musicPlayer)
        {
            musicPlayer.SetVolume(musicSlider.value);
        }
        else
        {
            Debug.LogWarning("No Music Player");
        }*/
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("Fullscreen is" + isFullScreen);

    }

    public void SaveAndExit()
    {
       // PlayerPrefsController.SetMusicVolume(musicSlider.value);
        FindObjectOfType<LevelLoader>().LoadMainMenuScene();
    }

    public void Defaults()
    {
        // Set
        MusicSlider.value = defaultMusic;
        MasterSlider.value = defaultMaster;
        EffectsSlider.value = defaultSFX;

        // Actually make the change in the mixers
    }

    public void QuitToMenu() {
        Time.timeScale = 1;
        GameManagerScript.GameManager.SceneManagerScritpto.SwitchScene(0, Vector2.zero);
    }
}
