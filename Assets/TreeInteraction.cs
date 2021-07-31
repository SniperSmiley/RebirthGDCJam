using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : IsInteractable
{
    public SpriteRenderer rend;
    public Color newcol;

    public override void Interact() {
        base.Interact();
        rend.color = newcol;
    }
}
