using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : IsInteractable
{
    public Sprite newSprite;

    private bool isTrunk = false;

    public override void Interact() {
        if (isTrunk) { return; }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();
        GameManagerScript.GameManager.Wood += 10f;
        rend.sprite = newSprite;
        isTrunk = true;
    }

}
