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
             //  Application.targetFrameRate = 80;
            first = true;
        }

    }

    public void ToggleControls(bool on) {
        if (on) { PlayerInputScript.Enable(); }
        else { PlayerInputScript.Disable(); }
    }
}
