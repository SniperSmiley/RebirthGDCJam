using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractScript : MonoBehaviour
{
    //PlayerInput _playerInputs = new PlayerInput();
    private bool AttemptInteract = false;

    

    public Transform RayCastFirePoint;
    public float InteractDistance;
    public LayerMask InteractMask;

    public Transform Sprite;

    private RaycastHit2D hit;

    private void Update() {
        Debug.DrawRay(RayCastFirePoint.position, -Vector2.left * Sprite.localScale.x * InteractDistance, Color.green, .1f);
    }

    public void AttemptInteraction(InputAction.CallbackContext value) {

        // Already interacting yeet
        if (AttemptInteract) { return; }

        AttemptInteract = true;

        hit = Physics2D.Raycast(RayCastFirePoint.position, -Vector2.left * Sprite.localScale.x , InteractDistance, InteractMask);

        Debug.DrawRay(RayCastFirePoint.position, -Vector2.left *  Sprite.localScale.x, Color.red, 5f);

        if (hit.collider != null) {
           // Debug.Log(hit.transform.name);
            if (hit.transform.GetComponent<IsInteractable>() ) {
                IsInteractable interact = hit.transform.GetComponent<IsInteractable>();
                interact.Interact();
            }
        }

        AttemptInteract = false;

     
    }
}
