using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : IsInteractable {

    public GameObject Player;
    public Animator EnemyAnimator;

    [SerializeField] private float _sightRange;
    [SerializeField] private float _attackRange;

    [SerializeField] private float _walkTopSpeed;
    [SerializeField] private float _walkAcceleration;

    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _chaseAcceleration;

    private enum EnemyStates {
        Idle,
        Roaming,
        Chasing,
        Attacking
    }
    private EnemyStates _currentState;

    private Rigidbody2D _enemyRig;
    private float _distFromPlayer;

    private Vector2 _enemyPos;
    private Vector2 _playerPos;

    private Vector2 _direction;


    // Idle walk
    [SerializeField] private float _MaxIdleWalkTime = 6;
    [SerializeField] private float _minIdleWalkTime = 1;
    private float _idleWalkTime = 0;
    private float _idleWalkTimeStart = 0;
    private bool _idleing = true;

    protected override void Awake() {
        base.Awake();

        _enemyRig = GetComponent<Rigidbody2D>();

    }

    protected override void Update() {

        base.Update();

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
                    _idleWalkTime /= 3f;
                }

                
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
        Debug.Log("CHASING");
    }

    public virtual void Attacking() {
        Debug.Log("ATTACKING");
    }

    public virtual void Roaming() {
        Debug.Log("Roaming");

        Vector2 newVel = _enemyRig.velocity + _direction * _walkAcceleration;

        if (newVel == _enemyRig.velocity) {
            // Potentially stuck? Change direction.
            Debug.Log("ASASSADASD");
            RandomiseDirection();
        }

        else if (newVel.magnitude < _walkTopSpeed) {
           
             _enemyRig.velocity = newVel;
        }
      

        Debug.Log(_enemyRig.velocity.magnitude);

    }

    public virtual void Idleing() {
        Debug.Log("IDLE");
    }



    private void RandomiseDirection() {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        _direction = new Vector3(x, y, 0f);
    }

}
