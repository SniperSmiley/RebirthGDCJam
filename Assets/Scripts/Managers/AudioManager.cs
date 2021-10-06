using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    [Header(" Effects Related ")]
    public AudioMixerGroup EffectsMixer;
    public int NumberOfAudioSources = 25;
    public List<AudioSource> AudioSourcesList = new List<AudioSource>();

    public AudioClip[] ChillTracks;

    public GameObject SoundEffectsGo;

    public AudioClip UISuccess;
    public AudioClip UIFail;
    public AudioClip UIHover;

    private bool ranStart = false;

    private void Awake() {

        if (ranStart) { return; }
        ranStart = true;

        AudioSource source;
        for (int i = 0; i < NumberOfAudioSources; i++) {
            source = SoundEffectsGo.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = EffectsMixer;
            AudioSourcesList.Add(source);
        }

    }

    AudioSource audioSource;
    bool trackPlaying = false;
    bool pausingBetweenTracks = false;

    int tracksPos = 0;
    float timePauseStarted;
    float timePause;

    private void Start() {
        //   DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = ChillTracks[tracksPos];
       // PlayTrack();
        
        StartCoroutine(PlayTrack());

        //audioSource.volume = PlayerPrefsController.GetMusicVolume();
    }

    private void Update() {
        
        // Check if audioclip is done. If it is, pause or play new etc.

        if (trackPlaying) { return;  }

        if (pausingBetweenTracks) {
            if (Time.time - timePauseStarted > timePause) {
                pausingBetweenTracks = false;

                if (tracksPos + 1 >= ChillTracks.Length ) { tracksPos = 0;  }
                else { tracksPos++; }

                tracksPos++;
                StartCoroutine(PlayTrack());
            }
        }

        else {
            // Wait for a couple seconds? 
            timePause = Random.Range(5, 30);
            pausingBetweenTracks = true;
            timePauseStarted = Time.time;
        }

       


        

    }

    private IEnumerator PlayTrack() {
        audioSource.clip = ChillTracks[tracksPos];
        audioSource.Play();
        trackPlaying = true;
        yield return new WaitForSeconds(audioSource.clip.length);
        trackPlaying = false;
    }

    public IEnumerator PlayEffect(AudioClip clip) {

        //Debug.Log("Play effect");
        AudioSource _audioSource = null;
        foreach (AudioSource source in AudioSourcesList) {
            if (source.clip == null) {
                // Not being used.
                _audioSource = source;
                break;
            }
        }

        if (_audioSource == null) { Debug.Log("Audio source ran out of sources."); }
        else {

            //Debug.Log("Play");
            _audioSource.clip = clip;
            _audioSource.Play();

            yield return new WaitForSeconds(_audioSource.clip.length);

            _audioSource.clip = null;

        }
    }

    public void SetVolume(float volume) {

        audioSource.volume = volume;
    }

}
