using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneInteractionScript : IsInteractable {


    public float health = 100f;

    public Sprite newSprite;

    private bool isCrushed = false;
    public CapsuleCollider2D col;

    public float StoneGive = 2f;

    protected override void Awake() {
        base.Awake();
    }

    public override void Interact() {


        if (isCrushed) { return; }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        health -= 20f * GameManagerScript.GameManager.PlayerStats.ResourceGatheringLevel;

        if (health <= 0) {
            OnGathered();
        }

        else { StartCoroutine(FlashColourFunc()); }

    }

    private void OnGathered() {

        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Stone, StoneGive);
        // GameManagerScript.GameManager.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Stone] += StoneGive;

        // Display Change
        string textToDisplay = "Stone + " + StoneGive;
        GameManagerScript.GameManager.resourceChangeDisplayScripto.DisplayChange(textToDisplay, transform.position);
        
        
        col.enabled = false;
        Rend.sprite = newSprite;
        isCrushed = true;
        base.Disabled = true;
        Rend.sortingOrder = 1;

    }

}
