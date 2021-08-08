using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarbonGeneratorScript : IsInteractable {


    public Animator anim;

    public GameObject Col1;
    public GameObject Col2;

    public CapsuleCollider2D col;
    private bool DisplayingUI = false;

    public float Delay = 1;
    private float LastTime = 0;

    protected override void Awake() {
        base.Awake();
    }

    protected override void Update() {
        base.Update();

        anim.SetInteger("Lvl", GameManagerScript.GameManager.UiManagerScripto.CarbonGenLevel);

        if (GameManagerScript.GameManager.UiManagerScripto.CarbonGenLevel > 0) {
            Col1.SetActive(false);
            Col2.SetActive(true);
        }
        else if (GameManagerScript.GameManager.UiManagerScripto.CarbonGenLevel > 10) {
            Col1.SetActive(true);
            Col2.SetActive(false);
        }

        if ( GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Carbon] > 0) {
            // make not gen
            anim.SetBool("Gen", true);
          
        }

        else {
            // Gen
            anim.SetBool("Gen", false);
        }

    /*   
       if (GameManagerScript.GameManager.UiManagerScripto.CarbonGenLevel > 10) { }
       else if (GameManagerScript.GameManager.UiManagerScripto.CarbonGenLevel > 5) { }
       else if (!GameManagerScript.GameManager.UiManagerScripto.IsGeneratorBroken) {
           
       }*/

    }

    public override void Interact() {

        if (!base.EnsureOnlyOneExecution()) { return; }

        if (GameManagerScript.GameManager.UiManagerScripto.CurrentActiveUI != 0) {  return;  }

        base.Interact();

       // Debug.Log("BEEP BOOP");
       

        GameManagerScript.GameManager.UiManagerScripto.ShowUI(UiManagerScript.UI.CarbonGenerator);
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


