using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectRenderingOrder : MonoBehaviour
{
    private EnvironmentObjectScript envScript;

  public void UpdateOrderFunc() {

        foreach (SpriteRenderer rend in transform.GetComponentsInChildren<SpriteRenderer>()) {

            // Find the objects Script. 
            envScript = rend.transform.GetComponentInParent<EnvironmentObjectScript>();
            if (envScript == null) {
                         rend.sortingOrder = Mathf.RoundToInt(100 -  rend.transform.position.y);
            }
            else {
                         rend.sortingOrder = Mathf.RoundToInt(100 -  envScript.SortingPos.position.y);
            }

   
        }

    }

}
