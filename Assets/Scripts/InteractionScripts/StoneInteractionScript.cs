using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneInteractionScript : IsInteractable {

    
    public Sprite newSprite;

    private bool isCrushed = false;
    public CapsuleCollider2D col;


    protected override void Awake() {
        base.Awake();
    }

    public override void Interact() {
       

        if (isCrushed) { return;  }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        GameManagerScript.GameManager.Stone += 10f;

        col.enabled = false;
        rend.sprite = newSprite;
        isCrushed = true;
        rend.sortingOrder = 1;

    }


}
