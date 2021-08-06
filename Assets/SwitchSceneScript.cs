using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneScript : MonoBehaviour
{

    public int grass = 0;
    public int SceneToSwitchTo = 0;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
              SceneManager.LoadScene(SceneToSwitchTo);
            GameManagerScript.GameManager.SceneManagerScritpto.SwitchScene(SceneToSwitchTo, GameObject.Find("Player").transform.position, grass);
        }
       
    }


}
