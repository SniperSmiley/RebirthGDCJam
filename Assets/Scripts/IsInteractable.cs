using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInteractable : MonoBehaviour
{

    public SpriteRenderer rend;

    private Color color;
    private Color newcol;
        
    public float CoolDown = 0.2f;
    private bool isCoolingDown = false;
    private float coolDownStartTime = 0;


    protected virtual void Awake() {
        color = rend.color;
        newcol = color;
        newcol.b = newcol.b - 0.3f;
    }


    public virtual void Interact() {

        // Ensure only interacted with once per click
        Debug.Log("Interacted with " + transform.name );
    }

    public bool EnsureOnlyOneExecution() {

        if (isCoolingDown) {

            if ((Time.time - coolDownStartTime) > CoolDown) { isCoolingDown = false; }
            else { return false;  }
        }

        isCoolingDown = true; coolDownStartTime = Time.time;
        return true;

    }

    public virtual void DisplayInteractable(bool display) {
       // Debug.Log("Intercting with " + transform.name );
        if (display) { rend.color = newcol; }
        else { rend.color = color; }
    }


}
