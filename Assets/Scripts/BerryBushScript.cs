using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBushScript : IsInteractable
{
  public override void Interact() {

        if (!base.EnsureOnlyOneExecution()) { return; }

        base.Interact();

        GameManagerScript.GameManager.Food += 10f;

        Destroy(gameObject);
    }
}
