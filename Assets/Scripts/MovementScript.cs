using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Rigidbody2D))]
public class MovementScript : MonoBehaviour
{

    public int BaseSortLayer = 500;
    public Transform PlayerSortingPos;

    public float MoveSpeed = 20f;

    private Rigidbody2D _playerRig;
    private Vector2 _input;


    private PlayerInput _playerInputs;
    //private S_MapManager _mapManager;

    private SpriteRenderer rend = null;


    private void Awake() {

        rend = GetComponentInChildren<SpriteRenderer>();
        _playerInputs = new PlayerInput(); _playerInputs.Enable();
       // _mapManager = FindObjectOfType<S_MapManager>();
        _playerRig = GetComponent<Rigidbody2D>();

    }

// Update is called once per frame
    void Update()
    {
        UpdateLayerOrder();
        CheckTile(); 
        GrabInput();

    }

    private void FixedUpdate() { MovePlayer(); }
       
    private void GrabInput() {
        _input = _playerInputs.Player.Move.ReadValue<Vector2>();
        _input.Normalize();
    }

    private void MovePlayer() {

        var movementOffset = _input * MoveSpeed / 10f;
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
        rend.sortingOrder = Mathf.RoundToInt((BaseSortLayer - PlayerSortingPos.position.y ) * 10 );
    }
}
