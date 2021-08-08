using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public AudioClip IntroSpeach;
    private MovementScript mov;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartIntro());
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
