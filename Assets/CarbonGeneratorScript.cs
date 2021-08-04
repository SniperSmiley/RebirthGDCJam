using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarbonGeneratorScript : IsInteractable {


    public CapsuleCollider2D col;
    private bool DisplayingUI = false;

    public float Delay = 1;
    private float LastTime = 0;

    protected override void Awake() {
        base.Awake();
    }

    protected override void Update() {
        base.Update();

        if (GameManagerScript.GameManager.UiManagerScripto.IsGeneratorBroken) { return;  }

        if (GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Carbon] >= 1) {
            if (Time.time - LastTime > Delay) {
                LastTime = Time.time;
                 GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Carbon] -= 1f;
                GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy] += GameManagerScript.GameManager.CarbonGeneratorEnergy ;
            }

       
        }

    }

    public override void Interact() {

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        Debug.Log("BEEP BOOP");

        GameManagerScript.GameManager.UiManagerScripto.ShowUI(UiManagerScript.UI.CarbonGenerator);
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


