using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInteractionScript : IsInteractable
{
    private bool DisplayingUI = false;

    public float Delay = 1;
    private float LastTime = 0;


    public override void Interact() {

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

       // Debug.Log("BEEP BOOP");

        GameManagerScript.GameManager.UiManagerScripto.ShowUI(UiManagerScript.UI.ShipUi);
        DisplayingUI = true;

    }

    public override void DisplayInteractable(bool display) {

        if (display == false) {
            DisplayingUI = false;
            GameManagerScript.GameManager.UiManagerScripto.CloseUI();
            Debug.Log("CLOSE");
        }

        base.DisplayInteractable(display);
    }

}
