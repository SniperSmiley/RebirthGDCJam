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


    public void SwitchScene(int scene, Vector2 Pos, int Grass = 0) {
        SceneManager.LoadScene(scene);

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
            case 0:

                // Forest
                if (grass == 1) { FindAndMovePlayer(new Vector2(pos.x, UpGrass)); }
                else if (grass == 2) { FindAndMovePlayer(new Vector2(RightGrass, pos.y)); }
                else if (grass == 3) { FindAndMovePlayer(new Vector2(pos.x, DownGrass)); }
                else if (grass == 2) { FindAndMovePlayer(new Vector2(LeftGrass, pos.y)); }


                break; // Grassland
            case 1: FindAndMovePlayer(new Vector2(pos.x, DownForest)); break;   // Forest,  Up from Grass
            case 2: FindAndMovePlayer(new Vector2(LeftSwamp, pos.y)); break;   // Swamp,   Rigth from grass
            case 3: FindAndMovePlayer(new Vector2(pos.x, TopDesert)); break;   // Desert   down from grass
            case 4: FindAndMovePlayer(new Vector2(RightTundra, pos.y)); break;   // Tundra   left from grass
        }
    }


    private void FindAndMovePlayer(Vector2 newPos) {

        Debug.Log("TES!!!T");
        Player = GameObject.Find("Player");
        if (Player == null) { Debug.Log("AAA"); }
        Player.transform.position = newPos;
    }
}
