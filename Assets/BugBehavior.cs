using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBehavior : MonoBehaviour
{
    public float speed = 1.0f;
    public float walkDist = 5.0f;
    public float attackDistance = 3.0f;
    public float waitTime = 3.0f;
    public float centered = 0.1f;

    private float currSpeed = 1.0f;
    private Vector2 startingPosition;
    private Animator animator;
    private Rigidbody2D rb2d;
    private Vector2 direction;
    private bool goingBack = true;
    private float elapsed = 0.0f;
    private float angle = 0.0f;
    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        startingPosition = rb2d.transform.position;
        animator = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed>waitTime)
        {
            elapsed = 0.0f;
            animator.SetBool("Walking",!animator.GetBool("Walking"));
        }
        //Player close to Bug
        //Player kills Bug

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BugWalk"))
        {
            Vector2 curr = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);
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
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BugDie"))
        {
            //die
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BugIdle"))
        {
            currSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = direction * currSpeed;
    }
}
