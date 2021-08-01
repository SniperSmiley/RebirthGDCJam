using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBushScript : IsInteractable
{
    public Sprite Bush;
    public Sprite BushWithBerries;
    public SpriteRenderer Rend;

    public bool IsBerries = true;

    protected override void Awake() {
        base.Awake();

        if (IsBerries) { Rend.sprite = BushWithBerries; }
        else { Rend.sprite = Bush; }

    }


    public override void Interact() {
        
        if (!IsBerries) { return; }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        GameManagerScript.GameManager.Food += 10f;

        IsBerries = false;
        Rend.sprite = Bush;

    }
}
