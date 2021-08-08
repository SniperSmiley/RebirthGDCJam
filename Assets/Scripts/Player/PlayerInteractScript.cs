using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractScript : MonoBehaviour {




    public PlayerAudioAndAnimations audio;

    //PlayerInput _playerInputs = new PlayerInput();
    private bool AttemptInteract = false;

    public Transform RayCastFirePoint;
    public float InteractDistance;
    public LayerMask InteractMask;

    public Transform Sprite;

    private RaycastHit2D hit;
    private IsInteractable target;

    private IsInteractable CurrentInteraction = null;

    private MovementScript moveScript;
    private Vector2 input;
    private Vector3 adjustedInput;

    private PlayerInput inputScript;

    private bool tryToInteract = false;

    private float LastInteractionTime = 0;

    private void Awake() {
        moveScript = GetComponent<MovementScript>();
        


    }

    private void Start() {
        inputScript = GameManagerScript.GameManager.InputManagerScript.PlayerInputScript;
        inputScript.Player.AttemptInteraction.started += context => IsInteracting(true);
        inputScript.Player.AttemptInteraction.canceled += context => IsInteracting(false);
    }

    private void Update() {
        // Debug.DrawRay(RayCastFirePoint.position, -Vector2.left * Sprite.localScale.x * InteractDistance, Color.green, .1f);
        input = moveScript._input;

        // Calculate the direction to fire ray. Whichever axis has highest magnitude
        if (input.magnitude > 0.01f) {
            // They have moved
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) {
                // The input leans towards the x axis more
                adjustedInput = new Vector3(input.x ,0, 0);
            }
            else {
                adjustedInput = new Vector3(0, input.y, 0);
            }
        }

        //Debug.Log(adjustedInput);


    }

    private void FixedUpdate() {
        hit = Physics2D.Raycast(RayCastFirePoint.position, adjustedInput * InteractDistance, InteractDistance, InteractMask);
        Debug.DrawRay(RayCastFirePoint.position, adjustedInput * InteractDistance, Color.red, 0.1f);

        if (hit.collider != null && hit.transform.GetComponent<IsInteractable>()) {

            CurrentInteraction = hit.transform.GetComponent<IsInteractable>();

            if (target != null) {

                // Was already look at a target
                if (target != CurrentInteraction) {
                    target.DisplayInteractable(false);
                }
            }

            //Debug.Log(hit.transform.name);
            CurrentInteraction.DisplayInteractable(true);
            target = CurrentInteraction;
        }

        else {
            if (CurrentInteraction != null) {
                CurrentInteraction.DisplayInteractable(false);
                CurrentInteraction = null;
            }
        }

        if (tryToInteract) { AttemptInteraction(); }
    }

    public void IsInteracting(bool yes) {
        if (yes) { tryToInteract = true;  }
        else { tryToInteract = false; }
    }



    //InputAction.CallbackContext value

    public void AttemptInteraction() {

      

       // Debug.Log("TEST");

        // Already interacting yeet
       // if (AttemptInteract) { return; }

       // AttemptInteract = true;

        if (CurrentInteraction != null) {


            if (!CurrentInteraction.isCoolingDown && CurrentInteraction.Disabled == false) {
                 // Audio Stuff here
                 audio.PlayCorrectAudio(CurrentInteraction);
            }
        

            CurrentInteraction.Interact(); 
        
        
        }



      //  AttemptInteract = false;


    }
}
