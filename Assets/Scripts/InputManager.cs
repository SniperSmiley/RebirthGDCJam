using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInput PlayerInputScript;


    private void Awake() {
        PlayerInputScript = new PlayerInput();
        PlayerInputScript.Enable();

        PlayerInputScript.Player.CloseMenu.performed += ctx => GameManagerScript.GameManager.UiManagerScripto.CloseUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
