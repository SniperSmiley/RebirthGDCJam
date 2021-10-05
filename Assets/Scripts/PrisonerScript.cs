using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerScript : MonoBehaviour {

    [SerializeField] private float _walkAcceleration = 4f;
    [SerializeField] private float _walkTopSpeed = 4f;

    [SerializeField] private float _minChangeDirectionTime = 2f;
    [SerializeField] private float _maxChangeDirectionTime = 2f;

    [SerializeField] private Transform _name;

    private float _currentTimeUntillDirectionChange = 1f;

    private float _timeLastChangedDirection = 0;

    private Rigidbody2D _rig;
    private Vector2 _direction;

    private float _timePotentiallyHitWall;

    [SerializeField] private float _minTimeSpeak = 2;
    [SerializeField] private float _maxTimeSpeak = 10;

    private float _timeOfLastSpeak = 0; // Speak on awake
    private float _timeBetweenSpeak = 0;

    public AudioClip[] VoiceLines;
    private AudioSource audioSource;


    private void Awake() {
        _rig = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
         _currentTimeUntillDirectionChange = Random.Range(_minChangeDirectionTime, _maxChangeDirectionTime);
    }

    private void Update() {
        if (Time.time - _timeOfLastSpeak > _timeBetweenSpeak) {
            // Speak
            if (VoiceLines.Length != 0) {

                int randNum = Random.Range(0, VoiceLines.Length);
                audioSource.clip = VoiceLines[randNum];
                audioSource.Play();
            }

            _timeOfLastSpeak = Time.time;
            _timeBetweenSpeak = Random.Range(2, _maxTimeSpeak);

        }
    }

    private void FixedUpdate() {

        // Randomise direction..
        if (Time.time - _timeLastChangedDirection > _currentTimeUntillDirectionChange) {
            // Okay sooo change the direction dumbass
            _currentTimeUntillDirectionChange = Random.Range(_minChangeDirectionTime, _maxChangeDirectionTime);
            _timeLastChangedDirection = Time.time;
            RandomiseDirection();
        }

        Vector2 newVel = _rig.velocity + (_direction * _walkAcceleration);

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

        if (newVel.magnitude <= _walkTopSpeed) { _rig.velocity = newVel; }

        Flip();
    }

    // Flip
    private void Flip() {

        // If moving 
        if (_rig.velocity.magnitude > 0) {

            // Right
            if (_rig.velocity.x > 0.12 && transform.localScale.x < 0) { transform.localScale = new Vector3(1f, 1f, 1f);  _name.localScale = new Vector3(1f, 1f, 1f); }

            // Left
            else if (_rig.velocity.x < -0.12 && transform.localScale.x > 0) { transform.localScale = new Vector3(-1f, 1f, 1f); _name.localScale = new Vector3(-1f, 1f, 1f); }
           
        }
    }

    private void RandomiseDirection() {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        _direction = new Vector3(x, y, 0f).normalized;
    }

}
