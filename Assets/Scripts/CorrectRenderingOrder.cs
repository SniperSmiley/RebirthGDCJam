using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectRenderingOrder : MonoBehaviour
{
    public int BaseLayer = 500;
    private EnvironmentObjectScript envScript;

    private void Awake() {

        // Ensures everthing is correctly ordered.
        UpdateOrderFunc();
    }


    public void UpdateOrderFunc() {
     
        foreach (SpriteRenderer rend in transform.GetComponentsInChildren<SpriteRenderer>()) {

            // Find the objects Script. 
            envScript = rend.transform.GetComponentInParent<EnvironmentObjectScript>();
            if (envScript == null) {
                         rend.sortingOrder = Mathf.RoundToInt((BaseLayer -  rend.transform.position.y) * 10);
            }
            else {
                         rend.sortingOrder = Mathf.RoundToInt((BaseLayer -  envScript.SortingPos.position.y) * 10);
            }

   
        }

    }

}
