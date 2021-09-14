using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrisonerSpawnerScript : MonoBehaviour
{
   // public PrisonerManagementUIScript PrisManageUI;

    private List<int> AwakePrisoners = new List<int>();

    public Transform PrisonerStorage;

    private void Start() {
        
    
    GameManagerScript.GameManager.PrisSpawner = this;
    }

    public void SpawnPrisoners() {
        
        // PrisManageUI = GameManagerScript.GameManager.UiManagerScripto.PrisonnerManagementUI.GetComponent<PrisonerManagementUIScript>();
    
        Debug.Log(GameManagerScript.GameManager.Prisoners.Count);

        // See how many are awake.
        for (int i = 0; i < GameManagerScript.GameManager.Prisoners.Count; i++) {
              if (GameManagerScript.GameManager.Prisoners[i].Awake) { AwakePrisoners.Add(i); }
        }

        Debug.Log(AwakePrisoners.Count);
        if (AwakePrisoners.Count == 0) { return; }

        // Choose random number of pris to spawn
        int randNumPris = Random.Range(0, AwakePrisoners.Count + 1);

        if (randNumPris == 0) { return; }

        // Randomly choose randNumPris number of times.
        for (int i = 0; i < randNumPris; i++) {
            int randomPris = Random.Range(0, AwakePrisoners.Count);
            PrisonerStorage.GetChild(AwakePrisoners[randomPris]).gameObject.SetActive(true);
            AwakePrisoners.RemoveAt(randomPris);
        }
    }

    public void SpawnPris(int id) {
         PrisonerStorage.GetChild(id).gameObject.SetActive(true);
    }

}
