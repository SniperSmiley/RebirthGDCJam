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

        if (IsGrown) { Rend.sprite = Grown; base.Disabled = false; }
        else { Rend.sprite = Chopped; base.Disabled = true; }

        timeOfInteract = Time.time;
    }

    protected override void Update() {

        base.Update();

        if ((Time.time - timeOfInteract) > ResfreshTime) {
            if (!IsGrown) {
                IsGrown = true;
                base.Disabled = false;
                Rend.sprite = Grown;
            }

        }
    }


    public override void Interact() {

        if (!IsGrown) { return; }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        OnGathered();

    }


    private void OnGathered() {


        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Carbon, CarbonGive);
        // GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Carbon] += CarbonGive;

        // Display Change
        string textToDisplay =  "Carbon + " + CarbonGive;
        GameManagerScript.GameManager.resourceChangeDisplayScripto.DisplayChange(textToDisplay, transform.position);

        timeOfInteract = Time.time;
        IsGrown = false;
        base.Disabled = true;
        Rend.sprite = Chopped;
    }

}




