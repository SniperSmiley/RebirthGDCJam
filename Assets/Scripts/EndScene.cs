using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndScene : MonoBehaviour
{
    public GameObject PrisonEnding;
    public GameObject FreedomEnding;

    public void DisplayPrison() {
        PrisonEnding.SetActive(true);
    }

    public void DisplayFreedomeee() {
        FreedomEnding.SetActive(true);
    }
    
}
