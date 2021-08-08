using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInput PlayerInputScript;

    public static bool first = false;

    private void Awake() {
        PlayerInputScript = new PlayerInput();
        PlayerInputScript.Enable();

        if (!first) {
                    PlayerInputScript.Player.CloseMenu.performed += ctx => GameManagerScript.GameManager.UiManagerScripto.OpenCloseOptions();
            first = true;
        }

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
