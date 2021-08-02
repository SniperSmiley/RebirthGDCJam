using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GameManager;

    public float Wood = 0;
    public float Stone = 0;
    public float Food = 50f;

    public AudioManager AudioManagerScript;

    private void Awake() {
        if (GameManager != null) { Destroy(gameObject); }
        else {
            GameManager = this;
            DontDestroyOnLoad(gameObject);
        }

        AudioManagerScript = GetComponentInChildren<AudioManager>();

    }
}
