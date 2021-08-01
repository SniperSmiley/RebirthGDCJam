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

    private IsInteractable CurrentInteraction = null;


    private void Update() {
        Debug.DrawRay(RayCastFirePoint.position, -Vector2.left * Sprite.localScale.x * InteractDistance, Color.green, .1f);
    }

    private void FixedUpdate() {
         hit = Physics2D.Raycast(RayCastFirePoint.position, -Vector2.left * Sprite.localScale.x , InteractDistance, InteractMask);

        Debug.DrawRay(RayCastFirePoint.position, -Vector2.left *  Sprite.localScale.x, Color.red, 5f);

        if (hit.collider != null && hit.transform.GetComponent<IsInteractable>()) {
           // Debug.Log(hit.transform.name);
           CurrentInteraction = hit.transform.GetComponent<IsInteractable>();
           CurrentInteraction.DisplayInteractable(true);
        }
        else { 

            if (CurrentInteraction != null) {
                CurrentInteraction.DisplayInteractable(false);
                CurrentInteraction = null; 
            }

           
        
        }
          
      
    }

    public void AttemptInteraction(InputAction.CallbackContext value) {

        // Already interacting yeet
        if (AttemptInteract) { return; }

        AttemptInteract = true;

        if (CurrentInteraction != null) { CurrentInteraction.Interact(); }
       
       

        AttemptInteract = false;

     
    }
}
