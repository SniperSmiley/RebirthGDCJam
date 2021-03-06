using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeInteraction : IsInteractable {


    public Animator TreeShake;

    public float health = 120f;

    public float WoodResourceGainFrom = 3f;
    public float CarbonResourceGainFrom = 2f;


    public Sprite newSprite;

    private bool isTrunk = false;

    public override void Interact() {
        if (isTrunk) { return; }

        base.Interact();

        if (!base.EnsureOnlyOneExecution()) { return; }

        //   Debug.Log("Interacted with " + transform.name);

        health -= 20f * GameManagerScript.GameManager.PlayerStats.ResourceGatheringLevel;

        if (health <= 0) {

            OnGathered();

          
        }

        else { TreeShake.SetTrigger("ShakeTree"); StartCoroutine(FlashColourFunc()); }

    }

     private void OnGathered() {

        // Change the values
        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Wood, WoodResourceGainFrom);
        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Carbon, CarbonResourceGainFrom);

        // Display Change
        string textToDisplay = "Wood + " + WoodResourceGainFrom + "\n" + "Carbon + " + CarbonResourceGainFrom;
        GameManagerScript.GameManager.resourceChangeDisplayScripto.DisplayChange(textToDisplay, transform.position);

        //GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Wood] += WoodResourceGainFrom;
        //GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Carbon] += CarbonResourceGainFrom;

        Rend.sprite = newSprite;
        isTrunk = true;
        base.Disabled = true;
    }


}
