using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOnRedirect : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D body;
    private Vector2 last;
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        last = body.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (body.position.x > last.x)
            gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        last = body.position;
    }
}
