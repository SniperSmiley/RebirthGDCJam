using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzuriteInteractionScript : IsInteractable {
    public float health = 140f;
    public float AzuriteGain = 2f;

    public Sprite newSprite;

    private bool isCrushed = false;
    public CapsuleCollider2D col;


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

        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Copper, AzuriteGain);
        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Stone, 1f);

        // Display Change
        string textToDisplay =  "Copper + " + AzuriteGain + "\n" + "Stone + " + 1;
        GameManagerScript.GameManager.resourceChangeDisplayScripto.DisplayChange(textToDisplay, transform.position);

        col.enabled = false;
        Rend.sprite = newSprite;
        isCrushed = true;
        base.Disabled = true;
        Rend.sortingOrder = 1;
    }

}
