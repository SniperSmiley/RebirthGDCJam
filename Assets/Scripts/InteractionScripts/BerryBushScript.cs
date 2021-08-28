using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBushScript : IsInteractable {
    public Sprite Bush;
    public Sprite BushWithBerries;
    public float ResfreshTime = 5f;

    public bool IsBerries = true;

    private float timeOfInteract = 0;

    public float BerryGive = 5f;

    protected override void Awake() {
        base.Awake();

        if (IsBerries) { Rend.sprite = BushWithBerries; base.Disabled = false; }
        else { Rend.sprite = Bush; base.Disabled = true; }

        timeOfInteract = Time.time;
    }

    protected override void Update() {

        base.Update();

        if ((Time.time - timeOfInteract) > ResfreshTime) {
            if (!IsBerries) {
                IsBerries = true;
                base.Disabled = false;
                Rend.sprite = BushWithBerries;
            }

        }
    }


    public override void Interact() {

        if (!IsBerries) { return; }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        OnGathered();

    }

    private void OnGathered() {

        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Food, BerryGive);

       // GameManagerScript.GameManager.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Food] += BerryGive;

        timeOfInteract = Time.time;
        IsBerries = false;
        base.Disabled = true;
        Rend.sprite = Bush;

    }
}
