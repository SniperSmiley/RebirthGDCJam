using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerWalk : MonoBehaviour
{
    public float speed = 1.0f;
    public float waitTime = 3.0f;
    public float centered = 0.1f;
    public float walkDist = 2.0f;
    private Animator animator;
    private float elapsed = 0.0f;
    private Rigidbody2D rb2d;
    private float currSpeed = 1.0f;
    private Vector2 direction;
    private Vector2 startingPosition;
    private float angle = 0.0f;
    private bool goingBack = true;
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        startingPosition = rb2d.transform.position;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
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

    private void FixedUpdate()
    {
        rb2d.velocity = direction * currSpeed;
        gameObject.GetComponent<SpriteRenderer>().flipX = (rb2d.velocity.x < 0.0f);
    }
}
