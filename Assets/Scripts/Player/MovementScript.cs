using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class MovementScript : MonoBehaviour {

    public bool isDisabled;

    public AudioClip[] FootSteps;

    public Animator PlayerAnimator;

    public int BaseSortLayer = 500;
    public Transform PlayerSortingPos;

    public float StartSpeed = 15f;
    public float MaxSpeed = 26f;
    public float Duration = 3f;

    private float moveSpeed;

    private Rigidbody2D _playerRig;
    public Vector2 _input;

    private bool isMoving = false;

    //private S_MapManager _mapManager;

    private SpriteRenderer rend = null;

    private float LastTimeFootStepPlayed = 0;
    private AudioManager audio;

    private void Awake() {

        rend = GetComponentInChildren<SpriteRenderer>();
        _playerRig = GetComponent<Rigidbody2D>();

    }

    private void Start() {
        audio = GameManagerScript.GameManager.AudioManagerScript;
    }

    // Update is called once per frame
    void Update() { UpdateLayerOrder(); CheckTile(); UpdateAnimation(); Footsteps(); }

    private void Footsteps() {
        if (!isMoving) { return; }

        if (Time.time - LastTimeFootStepPlayed > 0.3f) {
            StartCoroutine(audio.PlayEffect(FootSteps[Random.Range(0,FootSteps.Length)]));
            LastTimeFootStepPlayed = Time.time;
        }
    }

    public void FixedUpdate() { MovePlayer(); }

    public void GrabMovementInput(InputAction.CallbackContext value) {

        if (isDisabled) { return; }

        _input = value.ReadValue<Vector2>();
        _input.Normalize();
    }

    private void MovePlayer() {

        // Correct rotation
        if (Mathf.Abs(_input.x) > 0.1f) {
            if (_input.x > 0) { if (rend.transform.localScale.x != 1) { rend.transform.localScale = new Vector3(1f, 1f, 1f); } }
            else { if (rend.transform.localScale.x != -1) { rend.transform.localScale = new Vector3(-1f, 1f, 1f); } }
        }

        if (!isMoving && _input.magnitude > 0) {
            // Not started moving yet
            isMoving = true;
            StartCoroutine(AdjustSpeed());

        }
        else if (isMoving && _input.magnitude == 0) { isMoving = false; }



        var movementOffset = _input * moveSpeed / 10f;
        var newPos = _playerRig.position + movementOffset;
        //newPos = PixelPerfectClamp(newPos, 16);

        _playerRig.MovePosition(newPos);

    }

    private void CheckTile() {
        // MoveSpeed = _mapManager.GetTileWalkingSpeed(transform.position) / 10f;
    }


    // WIll probs remove
    private Vector2 PixelPerfectClamp(Vector2 moveVector, float ppu) {
        Vector2 vectorInPixels = new Vector2(
                Mathf.RoundToInt(moveVector.x * ppu),
                Mathf.RoundToInt(moveVector.y * ppu));

        return vectorInPixels / ppu;

    }

    private void UpdateLayerOrder() {
        // Times 10 to make it have more layes of accuracy. Instead of per grid space.
        rend.sortingOrder = Mathf.RoundToInt((BaseSortLayer - PlayerSortingPos.position.y) * 10);
    }

    private void UpdateAnimation() {

        PlayerAnimator.SetBool("IsMoving", isMoving);

        if (_input.magnitude > 0.01f) {
            // They have moved
            if (Mathf.Abs(_input.x) > Mathf.Abs(_input.y)) {
                // The input leans towards the x axis more
                PlayerAnimator.SetInteger("State", 3);
            }
            else {

                // If going left or right as well

                if (Mathf.Abs(_input.x) > 0.2f) {PlayerAnimator.SetInteger("State", 3); }
                else {
                            // check if up or down
                    if (_input.y > 0.1f) {
                        PlayerAnimator.SetInteger("State", 1);
                    }
                    else {
                        PlayerAnimator.SetInteger("State", 2);
                    }
                }
            
            }
        }
    }

    private IEnumerator AdjustSpeed() {

        float elapsed = 0.0f;
        //Debug.Log("1" + moveSpeed);

        while (elapsed < Duration) {


            if (isMoving == false) {
                // break out, the player is no longer moving so reset values.
                moveSpeed = StartSpeed;
                //Debug.Log("BROKEOUT");
                yield break;
            }

            moveSpeed = Mathf.Lerp(StartSpeed, MaxSpeed, elapsed / Duration);
            //Debug.Log(moveSpeed);
            elapsed += Time.deltaTime;
            yield return null;

        }

        moveSpeed = MaxSpeed;

    }

}
