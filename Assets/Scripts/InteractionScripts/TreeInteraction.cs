using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeInteraction : IsInteractable {

    public float health = 100f;

    public Sprite newSprite;

    private bool isTrunk = false;

    public override void Interact() {
        if (isTrunk) { return; }

        base.Interact();
   
        if (!base.EnsureOnlyOneExecution()) { return; }

       //   Debug.Log("Interacted with " + transform.name);

        health -= 25f;

        if (health <= 0) {
            GameManagerScript.GameManager.Wood += 10f;
            rend.sprite = newSprite;
            isTrunk = true;
        }

        else { StartCoroutine(FlashColourFunc()); }

    }


}
