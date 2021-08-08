using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruxiteInteractionScript : IsInteractable
{

    public float health = 100f;

    public Sprite newSprite;

    private bool isCrushed = false;
    public CapsuleCollider2D col;

    public float StoneGive = 2f;
    public float BruxiteGive = 4f;

    protected override void Awake() {
        base.Awake();
    }

    public override void Interact() {
       

        if (isCrushed) { return;  }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        health -= 20f * GameManagerScript.GameManager.PlayerStats.ResourceGatheringLevel;

        if (health <= 0) {
            GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Stone]   += StoneGive;
            GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Buxite]  += BruxiteGive;
            col.enabled = false;
            Rend.sprite = newSprite;
            isCrushed = true;
                   base.Disabled = true;
            Rend.sortingOrder = 1;
        }

        else { StartCoroutine(FlashColourFunc()); }

    }
}
