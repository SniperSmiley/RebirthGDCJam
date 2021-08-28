using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronInteractionScript : IsInteractable
{


    public float health = 140f;
    public float IronGain = 2f;

    public Sprite newSprite;

    private bool isCrushed = false;
    public CapsuleCollider2D col;


    protected override void Awake() {
        base.Awake();
    }

    public override void Interact() {
       

        if (isCrushed) { return;  }

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        health -= 20f * GameManagerScript.GameManager.PlayerStats.ResourceGatheringLevel;

        if (health <= 0) {

            OnGathered();

          
        }

        else { StartCoroutine(FlashColourFunc()); }

    }

    private void OnGathered() {

        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Iron, IronGain);
        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Stone, 1f);

       // GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Iron] += IronGain;
        //GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Stone] += 1f;

        col.enabled = false;
        Rend.sprite = newSprite;
        isCrushed = true;
        base.Disabled = true;
        Rend.sortingOrder = 1;
    }
}
