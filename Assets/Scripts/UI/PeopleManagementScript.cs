using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManagementScript : IsInteractable
{
    public CapsuleCollider2D col;
    private bool DisplayingUI = false;

    public float Delay = 1;
    private float LastTime = 0;

    protected override void Awake() {
        base.Awake();
    }

    protected override void Update() {
        base.Update();
    }

    public override void Interact() {

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        Debug.Log("BEEP BOOP");

        GameManagerScript.GameManager.UiManagerScripto.ShowUI(UiManagerScript.UI.PrisonerManagement);
        DisplayingUI = true;

    }

    public override void DisplayInteractable(bool display) {

        if (display == false) {
            DisplayingUI = false;
            GameManagerScript.GameManager.UiManagerScripto.CloseCurrentMenu();
            Debug.Log("CLOSE");
        }

        base.DisplayInteractable(display);
    }
}
