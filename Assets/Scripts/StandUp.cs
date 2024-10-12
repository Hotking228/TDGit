using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private void Start()
    {
        rb = transform.root.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        transform.up = Vector2.up;
        sr.flipX = rb.velocity.x > 0 ? false : true;
    }
}
