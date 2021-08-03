using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBushScript : IsInteractable
{
    public Sprite Bush;
    public Sprite BushWithBerries;
    public SpriteRenderer Rend;
    public float ResfreshTime = 5f;

    public bool IsBerries = true;

    private float timeOfInteract = 0;

    protected override void Awake() {
        base.Awake();

        if (IsBerries) { Rend.sprite = BushWithBerries; }
        else { Rend.sprite = Bush; }

        timeOfInteract = Time.time;
    }

    protected override void Update() {

        base.Update();

        if ((Time.time - timeOfInteract) > ResfreshTime ) {
            if (!IsBerries) {
                IsBerries = true;
                Rend.sprite = BushWithBerries;
            }
        
        }
    }


    public override void Interact() {
        
        if (!IsBerries) { return; }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        GameManagerScript.GameManager.Food += 10f;

        timeOfInteract = Time.time;
        IsBerries = false;
        Rend.sprite = Bush;

    }
}
