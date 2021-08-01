using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneInteractionScript : IsInteractable {

    
    public Sprite newSprite;

    private bool isCrushed = false;
    private CapsuleCollider2D col;


    protected override void Awake() {
        base.Awake();
       col = GetComponent<CapsuleCollider2D>();
    }

    public override void Interact() {
       

        if (isCrushed) { return;  }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        GameManagerScript.GameManager.Stone += 10f;

        col.enabled = false;
        rend.sprite = newSprite;
        isCrushed = true;

    }

}
