using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeMovement : MonoBehaviour
{
    public GameObject head;
    public GameObject segment;
    public GameObject segment1;
    public GameObject segment2;
    public GameObject segment3;
    public GameObject segment4;
    public GameObject tail;
    public float scale;
    public float speed;
    public float angle;
    public Vector2 direction;
    public Rigidbody2D rb2d;
    private float elapsed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed = 0.0f;
            angle += Random.Range(-30.0f, 30.0f);
            direction.x = Mathf.Cos(angle / 180.0f * Mathf.PI);
            direction.y = Mathf.Sin(angle / 180.0f * Mathf.PI);
        }
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + direction * speed / 10f);
    }
}
