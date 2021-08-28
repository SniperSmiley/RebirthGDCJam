using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBehavior : IsInteractable
{
    public float health = 100;

    public float speed = 1.0f;
    public float chargeSpeed = 3.0f;
    public float walkDist = 5.0f;
    public float attackDistance = 3.0f;
    public float waitTime = 3.0f;
    public float centered = 0.1f;
    public GameObject player;

    private float currSpeed = 1.0f;
    private Vector2 startingPosition;
    private Animator animator;
    private Rigidbody2D rb2d;
    private Rigidbody2D playerRb2d;
    private Vector2 direction;
    private bool goingBack = true;
    private bool charging = false;
    private bool charge = false;
    private float elapsed = 0.0f;
    private float angle = 0.0f;

    bool dead = false;

    protected override void  Awake()
    {
        base.Awake();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        startingPosition = rb2d.transform.position;
        animator = gameObject.GetComponent<Animator>();
        playerRb2d = player.GetComponent<Rigidbody2D>();
    }

    protected override void Update() {

        if (dead) { OnDeath();}

        base.Update();

        if (health <= 0) { animator.SetBool("Die", true); currSpeed = 0; dead = true;;  return; }
        // Check if dead 

        
        Vector2 curr = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
        Vector2 playerCurr = new Vector2(playerRb2d.transform.position.x, playerRb2d.transform.position.y);
        float distToPlayer = Vector2.Distance(playerRb2d.transform.position, curr);
        elapsed += Time.deltaTime;

        //Player close to Bug
        if (!charging && distToPlayer < attackDistance)
        {
            elapsed = 0.0f;
            animator.SetBool("Charging", true);
            charging = true;
        }
        if (elapsed > waitTime)
        {
                elapsed = 0.0f;
            if (!charging)
            {
                animator.SetBool("Walking", !animator.GetBool("Walking"));
            }
            else if (!charge)
            {
                charge = true;
            }
            else
            {
                charge = false;
                charging = false;
                animator.SetBool("Charging", false);
            }
        }
        //Player kills Bug

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BugWalk"))
        {
            float diff = Vector2.Distance(startingPosition, curr);
            if (diff < centered && goingBack)
            {
                angle = Random.Range(-360.0f, 360.0f);
                direction.x = Mathf.Cos(angle / 180.0f * Mathf.PI);
                direction.y = Mathf.Sin(angle / 180.0f * Mathf.PI);
                goingBack = false;
            }
            if (diff > walkDist)
            {
                direction = (startingPosition - curr).normalized;
                goingBack = true;
            }
            currSpeed = speed;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("AngryBug"))
        {
            //charge the player
            if (!charge)
            {
                currSpeed = 0.00001f;
            }
            else
            {
                direction = (playerCurr - curr).normalized;
                currSpeed = chargeSpeed;
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BugDie"))
        {
            //die
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BugIdle"))
        {
            currSpeed = 0.0001f;
        }
    }

    private void OnDeath() {
        base.DisplayInteractOveride(false); 
        Destroy(this); 
    }

    

    private void FixedUpdate()
    {
        rb2d.velocity = direction * currSpeed;

        Flip();

        //gameObject.GetComponent<SpriteRenderer>().flipX = (rb2d.velocity.x < 0.0f);
    }

    // Flip
    public void Flip() {

        // If moving 
        if (rb2d.velocity.magnitude > 0) {

            // Right
            if (rb2d.velocity.x > 0 && transform.localScale.x < 0) {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            // Left
            else if  (rb2d.velocity.x < 0 && transform.localScale.x > 0){
                 transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }


     public override void Interact() {
       
        // Only runs once
        if (!base.EnsureOnlyOneExecution()) { return; }

        // base.Interact();
        // Attack
        health -= 10;


        //else { StartCoroutine(FlashColourFunc()); }

    }

    public override void DisplayInteractable(bool display) {
       base.DisplayInteractable(display);
        Debug.Log("TEST");
    }

}
