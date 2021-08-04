using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractionScript : IsInteractable {

    public Sprite Chopped;
    public Sprite Grown;

    public float CarbonGive = 5f;
    public float ResfreshTime = 5f;

    public bool IsGrown = true;

    private float timeOfInteract = 0;

    protected override void Awake() {
        base.Awake();

        if (IsGrown) { Rend.sprite = Grown; }
        else { Rend.sprite = Chopped; }

        timeOfInteract = Time.time;
    }

    protected override void Update() {

        base.Update();

        if ((Time.time - timeOfInteract) > ResfreshTime ) {
            if (!IsGrown) {
                IsGrown = true;
                Rend.sprite = Grown;
            }
        
        }
    }


    public override void Interact() {
        
        if (!IsGrown) { return; }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Carbon] += CarbonGive;

        timeOfInteract = Time.time;
        IsGrown = false;
        Rend.sprite = Chopped;

    }

}


    

