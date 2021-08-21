using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroScene : MonoBehaviour
{
    public AudioClip IntroSpeach;
    private MovementScript mov;

    public VideoPlayer vidPlay;
    string filePath;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartIntro());

        filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Cut.mp4");
        vidPlay.url = filePath;

        vidPlay.renderMode = VideoRenderMode.RenderTexture;
        vidPlay.targetCameraAlpha = 1.0f;
        vidPlay.Prepare();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
                                                                        
    public  IEnumerator StartIntro() {
        
        // Not loaded scene
        while (SceneManager.GetActiveScene().buildIndex != 2) {
            yield return null;
        }

        Debug.Log("SCENE");
        vidPlay.Play();
        mov = GameObject.Find("Player").GetComponent<MovementScript>();
        mov.isDisabled = true;
        Debug.Log(mov.name);

        GameManagerScript.GameManager.InputManagerScript.ToggleControls(false);
        


        yield return new WaitForSeconds(25);


        
        StartCoroutine(GameManagerScript.GameManager.AudioManagerScript.PlayEffect(IntroSpeach));
        mov.isDisabled = false;                     
        GameManagerScript.GameManager.InputManagerScript.ToggleControls(true);


        gameObject.SetActive(false);
      


    }
}
