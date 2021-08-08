using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {
    public GameObject Player;

    public float RightGrass;
    public float LeftGrass;
    public float DownGrass;
    public float UpGrass;

    public float DownForest;
    public float LeftSwamp;
    public float TopDesert;
    public float RightTundra;


    private void Start() {
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1) {
              GameManagerScript.GameManager.UiManagerScripto.OnPlayingUI(true);
        }
    }

    public void SwitchScene(int scene, Vector2 Pos, int Grass = 0) {
           GameManagerScript.GameManager.UiManagerScripto.CloseCurrentMenu();
        SceneManager.LoadScene(scene);
     
        if (scene == 0 || scene == 1) {
            GameManagerScript.GameManager.UiManagerScripto.OnPlayingUI(false);
        }
        else {
            GameManagerScript.GameManager.UiManagerScripto.OnPlayingUI(true);
        }
  

        if (Pos == new Vector2(0, 0)) {
            return;
        }

        if (SceneManager.GetActiveScene().buildIndex != scene) {
            StartCoroutine(WaitForSceneToLoad(scene, Pos, Grass));
        }
        // Wait for scene to load.
    }

    public IEnumerator WaitForSceneToLoad(int Scene, Vector2 pos, int grass) {

        while (SceneManager.GetActiveScene().buildIndex != Scene) {
            yield return null;
        }

        // Scene Loaded!

        // take their position of entry, calculate their new position;=

        switch (Scene) {
            case 2:

                // Forest
                if (grass == 1) { FindAndMovePlayer(new Vector2(pos.x, UpGrass)); }
                else if (grass == 2) { FindAndMovePlayer(new Vector2(RightGrass, pos.y)); }
                else if (grass == 3) { FindAndMovePlayer(new Vector2(pos.x, DownGrass)); }
                else if (grass == 4) { FindAndMovePlayer(new Vector2(LeftGrass, pos.y)); }


                break; // Grassland
            case 3: FindAndMovePlayer(new Vector2(pos.x, DownForest)); break;   // Forest,  Up from Grass
            case 4: FindAndMovePlayer(new Vector2(LeftSwamp, pos.y)); break;   // Swamp,   Rigth from grass
            case 5: FindAndMovePlayer(new Vector2(pos.x, TopDesert)); break;   // Desert   down from grass
            case 6: FindAndMovePlayer(new Vector2(RightTundra, pos.y)); break;   // Tundra   left from grass
        }
    }


    private void FindAndMovePlayer(Vector2 newPos) {

       // Debug.Log("TES!!!T");
        Player = GameObject.Find("Player");

        Player.transform.position = newPos;
    }
}
