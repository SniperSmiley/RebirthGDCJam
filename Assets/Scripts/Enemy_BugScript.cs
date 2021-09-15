using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BugScript : EnemyScript {

    private float originalHelf;
    public Transform Helf;

    [SerializeField] private Color _ChargeUpColour;
    [SerializeField] private Color _ChargeColour;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private float _attackDuration = 2f;
    [SerializeField] private float _attackChargeUpTime = .2f;
    [SerializeField] private float _attackChargeSpeed = 5f;

    private Color startColor;
    private int FirstOff = 0;
    private float _timeAttackStarted = 0;
    private float _timeOfLastAttack = 0;


    protected override void Awake() {


        base.Awake();
        startColor = Rend.color;
        originalHelf = health;
    }

    public override void Attacking() {
        base.Attacking();
        // Attack

        // Attack cooldown
        if (Time.time - _timeOfLastAttack <= _attackCooldown) { base.Chasing(); return; }

        // Starting attack animation
        // on first time work out direction
        // Start
        if (FirstOff == 0) {
            FirstOff ++;
            _lockedInState = true;
            _direction = (_playerPos - _enemyPos).normalized;
            _enemyRig.velocity = Vector2.zero;
            // Set colour red.
           
            _timeAttackStarted = Time.time;
            base.PreventInteractionColorChange = true;
        }

        // Charging up
        if (Time.time - _timeAttackStarted <= _attackChargeUpTime) {
            if (FirstOff == 1) {
                 FirstOff ++;
                 base.Rend.color = _ChargeUpColour;
            }
        }

        // Charging
        else if (Time.time - _timeAttackStarted <= _attackDuration + _attackChargeUpTime) {

            if (FirstOff == 2) {
                 FirstOff ++;
                 base.Rend.color = _ChargeColour;
            }

            _enemyRig.velocity = _direction * _attackChargeSpeed;

            // Deal damage if the player is in the way.
        }

        // End
        else {
            FirstOff = 0;
            _lockedInState = false;
            _enemyRig.velocity = Vector2.zero;
            _timeOfLastAttack = Time.time;
            base.Rend.color = startColor;
            base.PreventInteractionColorChange = false;
        }



    }

    public override void Interact() {

        if (!base.EnsureOnlyOneExecution()) { return; }
            
        base.Interact();

       //ebug.Log("ENEMY SCRIPT");

        string textToDisplay = "OUCH! - 25";
        GameManagerScript.GameManager.resourceChangeDisplayScripto.DisplayChange(textToDisplay, transform.position);

        // Display Health
        Helf.localScale = new Vector3(base.health / originalHelf, 1f, 1f);

        
    }

    public override void OnDeath() {
        base.OnDeath();
        Helf.gameObject.transform.parent.gameObject.SetActive(false);
    }


}
