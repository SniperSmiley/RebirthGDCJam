using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneInteractionScript : IsInteractable {


    public float health = 100f;

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

        health -= 20f * GameManagerScript.GameManager.PlayerStats.ResourceGatheringLevel;

        if (health <= 0) {
            GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Stone]  += 2f;
            col.enabled = false;
            Rend.sprite = newSprite;
            isCrushed = true;
            Rend.sortingOrder = 1;
        }

        else { StartCoroutine(FlashColourFunc()); }

    }


}
