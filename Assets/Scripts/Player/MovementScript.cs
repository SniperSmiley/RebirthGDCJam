using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class MovementScript : MonoBehaviour {

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

    private void Awake() {

        rend = GetComponentInChildren<SpriteRenderer>();
        _playerRig = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() { UpdateLayerOrder(); CheckTile(); }

    public void FixedUpdate() { MovePlayer(); }

    public void GrabMovementInput(InputAction.CallbackContext value) {
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



        var movementOffset = _input * moveSpeed  / 10f;
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

    private IEnumerator AdjustSpeed() {

        float elapsed = 0.0f;
        Debug.Log("1" + moveSpeed);

        while (elapsed < Duration) {


            if (isMoving == false) {
                // break out, the player is no longer moving so reset values.
                moveSpeed = StartSpeed;
                Debug.Log("BROKEOUT");
                yield break;
            }

            moveSpeed = Mathf.Lerp(StartSpeed, MaxSpeed, elapsed / Duration);
            Debug.Log(moveSpeed);
            elapsed += Time.deltaTime;
            yield return null;

        }

        moveSpeed = MaxSpeed;

    }

}
