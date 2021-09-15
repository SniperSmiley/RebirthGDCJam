using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : IsInteractable {

    public float health;
    public GameObject Player;
    public Animator EnemyAnimator;

    public float CarbonGive = 6f;
    public float FoodGive = 10f;

    [SerializeField] private float _sightRange;
    [SerializeField] private float _attackRange;

    [SerializeField] private float _walkTopSpeed;
    [SerializeField] private float _walkAcceleration;

    [SerializeField] private float _chaseAcceleration;

    private enum EnemyStates {
        Idle,
        Roaming,
        Chasing,
        Attacking
    }
    private EnemyStates _currentState;

    protected Rigidbody2D _enemyRig;
    protected float _distFromPlayer;
    protected Vector2 _enemyPos;
    protected Vector2 _playerPos;
    protected Vector2 _direction;

    // Idle walk
    [SerializeField] private float _MaxIdleWalkTime = 6;
    [SerializeField] private float _minIdleWalkTime = 1;
    private float _idleWalkTime = 0;
    private float _idleWalkTimeStart = 0;
    private bool _idleing = true;
    private bool switchdirection = true;
    private float _timePotentiallyHitWall = 0;

    protected bool _lockedInState = false;

    protected override void Awake() {
           

        base.Awake();

        _enemyRig = GetComponent<Rigidbody2D>();


    }

    protected override void Update() {

        base.Update();

        // Prevent swithing state in attack animation etc,
        if (_lockedInState) { return; }

        _playerPos = new Vector2(Player.transform.position.x, Player.transform.position.y);
        _enemyPos = new Vector2(transform.position.x, transform.position.y);

        // Work out what state the enemy should be in.
        _distFromPlayer = (_playerPos - _enemyPos).magnitude;

        // Check if player in sight range, Try attack them
        if (_distFromPlayer <= _attackRange) { _currentState = EnemyStates.Attacking; }

        // Check if player in Attack range, Chase them
        else if (_distFromPlayer <= _sightRange) { _currentState = EnemyStates.Chasing; }

        // Roam freely, either Idle or walk around.
        else {

            // If first time, Idle for random duration of time. ( _idleWalkTime ) If time is passed switch to walking and keep switching. If other things interfere, continue where left off.

            // Time has passed, switch mode
            if (Time.time - _idleWalkTimeStart > _idleWalkTime) {

                _idleWalkTime = Random.Range(_minIdleWalkTime, _MaxIdleWalkTime);

                if (_idleing) {
                    _currentState = EnemyStates.Roaming;
                    RandomiseDirection();
                    _idleing = false;
                }
                else {
                    _currentState = EnemyStates.Idle;
                    _idleing = true;
                    _idleWalkTime /= 5f;
                }

                switchdirection = true;
                _idleWalkTimeStart = Time.time;
            }

            // Otherwise, continue where left off
            else {
                if (_idleing) { _currentState = EnemyStates.Idle; }
                else { _currentState = EnemyStates.Roaming; }
            }

        }
    }

    // Perform the movements made this frame.
    protected virtual void FixedUpdate() {

        Flip();

        // Now we know the state, run the appropriate function.
        switch (_currentState) {
            case EnemyStates.Idle: Idleing(); break;
            case EnemyStates.Roaming: Roaming(); break;
            case EnemyStates.Chasing: Chasing(); break;
            case EnemyStates.Attacking: Attacking(); break;
            default: break;
        }
    }



    public virtual void Chasing() {
        //Debug.Log("CHASING");
        if (!EnemyAnimator.GetBool("Charging")) { EnemyAnimator.SetBool("Charging", true); }

        // Set direction to look at player
        _direction = (_playerPos - _enemyPos).normalized;

        // Add velocity

        _enemyRig.velocity = _direction * _chaseAcceleration;
    }

    public virtual void Attacking() {
       // Debug.Log("ATTACKING");

        // Attack is different for each creature.

        // Charge in last direction.
    }

    public virtual void Roaming() {
        //Debug.Log("Roaming");

        // Set animation
        if (EnemyAnimator.GetBool("Charging")) { EnemyAnimator.SetBool("Charging", false); }
        if (!EnemyAnimator.GetBool("Walking")) { EnemyAnimator.SetBool("Walking", true); }

        // Halfway through walking change direction.
        if (Time.time - _idleWalkTimeStart > _idleWalkTime / 2 && switchdirection) {
            RandomiseDirection();
            switchdirection = false;
        }

        Vector2 newVel = _enemyRig.velocity + (_direction * _walkAcceleration);

        // Check if potentially stuck? The velocity will roughly equal the acceleration
        if (newVel.magnitude <= _walkAcceleration + .2f) {
            // If first
            if (_timePotentiallyHitWall == 0) {
                _timePotentiallyHitWall = Time.time;
            }
            else if (Time.time - _timePotentiallyHitWall > 0.20) {
                RandomiseDirection();
                _timePotentiallyHitWall = 0;
                Debug.Log("STUCK");
            }
        }

        if (newVel.magnitude <= _walkTopSpeed) { _enemyRig.velocity = newVel; }
        //  else { Debug.Log("REE" + _enemyRig.velocity.magnitude); }

        // Debug.Log(newVel.magnitude  + " "+_enemyRig.velocity.magnitude + " " + _direction);

    }

    public virtual void Idleing() {
        // Debug.Log("IDLE");
        if (EnemyAnimator.GetBool("Walking")) { EnemyAnimator.SetBool("Walking", false); }
        if (EnemyAnimator.GetBool("Charging")) { EnemyAnimator.SetBool("Charging", false); }

        if (_enemyRig.velocity.magnitude <= 0.2) {
            _enemyRig.velocity = Vector2.zero;
        }
        else if (_enemyRig.velocity.magnitude > 0) {
            _enemyRig.velocity /= 2;
        }
   
    }

    private void RandomiseDirection() {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        _direction = new Vector3(x, y, 0f).normalized;
    }
    // Flip
    private void Flip() {

        // If moving 
        if (_enemyRig.velocity.magnitude > 0) {

            // Right
            if (_enemyRig.velocity.x > 0.12 && transform.localScale.x < 0) { transform.localScale = new Vector3(1f, 1f, 1f); }

            // Left
            else if (_enemyRig.velocity.x < -0.12 && transform.localScale.x > 0) { transform.localScale = new Vector3(-1f, 1f, 1f); }
        }
    }


    public override void Interact() {


      

        // Only runs once
       //f (!base.EnsureOnlyOneExecution()) { return; }

       //ebug.Log("ENEMY SCRIPT 1");

        // base.Interact();
        // Attack

        // Stop movement
        _enemyRig.velocity = Vector2.zero;

        StartCoroutine(base.FlashColourFunc(Color.grey, .5f));
        health -= 10;

        if (health <= 0) {
            OnDeath();
        }


        //else { StartCoroutine(FlashColourFunc()); }

    }

    public virtual void OnDeath() {

        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Carbon, CarbonGive);
        GameManagerScript.GameManager.AddResourceToInventory(Resources.ResourcesIndex.Food, FoodGive);
        // GameManagerScript.GameManager.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Stone] += StoneGive;

        // Display Change
        string textToDisplay = "Carbon + " + CarbonGive +"\n" + "Food + " + FoodGive;
        GameManagerScript.GameManager.resourceChangeDisplayScripto.DisplayChange(textToDisplay, transform.position);
        

        _enemyRig.velocity = Vector3.zero;
        EnemyAnimator.SetBool("Die", true);
        DisplayInteractOveride(false); // Set normal colour.
        _enemyRig.sharedMaterial = null;
        _enemyRig.drag = 3f;
        Destroy(this);
    }

    public override void DisplayInteractable(bool display) {
        base.DisplayInteractable(display);
        Debug.Log("TEST");
    }
}
