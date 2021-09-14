using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInteractable : MonoBehaviour {
    public SpriteRenderer Rend;

    public bool Disabled = false;

    private Color FlashColour;
    private Color color;
    private Color newcol;

    public float CoolDown = 0.2f;
    public bool isCoolingDown = false;
    private float coolDownStartTime = 0;

    public bool PreventInteractionColorChange = false;
    private bool _lockColour = false;

    protected virtual void Awake() {
        color = Rend.color;
        newcol = color;
        newcol.b = newcol.b - 0.3f;
        FlashColour = newcol;
        FlashColour.a = newcol.a - 0.18f;
    }

    protected virtual void Update() {
         if (isCoolingDown) {

            if ((Time.time - coolDownStartTime) >= CoolDown) { isCoolingDown = false; }
            else { return; }
        }
    }


    public virtual void Interact() {

        // Ensure only interacted with once per click
        
    }


    public bool EnsureOnlyOneExecution() {

        if (isCoolingDown) { return false;  }

        isCoolingDown = true; coolDownStartTime = Time.time;

        return true;

    }

    public virtual void DisplayInteractable(bool display) {

        if (isCoolingDown) { return; }
        if (PreventInteractionColorChange) { return; }    

        // Debug.Log("Intercting with " + transform.name );
        if (_lockColour) { return; }
        if (display) { Rend.color = newcol; }
        else { Rend.color = color; }
    }



    public virtual IEnumerator FlashColourFunc() {

        if (_lockColour) { yield return null; }

        Rend.color = FlashColour;

        yield return new WaitForSeconds(CoolDown - 0.1f);

        Rend.color = color;
    }

    public virtual IEnumerator FlashColourFunc(Color col, float time) {
        _lockColour = true;
        Rend.color = col;

        yield return new WaitForSeconds(time);

        Rend.color = color;
        _lockColour = false;
    }

    public void DisplayInteractOveride(bool display) {
        if (_lockColour) { return; }
        if (display) { Rend.color = newcol; }
        else { Rend.color = color; }
    }


}
