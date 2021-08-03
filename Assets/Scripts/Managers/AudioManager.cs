using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [Header(" Effects Related ")]
    public AudioMixerGroup EffectsMixer;
    public int NumberOfAudioSources = 25;
    public List<AudioSource> AudioSourcesList = new List<AudioSource>();

     public IEnumerator PlayEffect(AudioClip clip) {

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
}
