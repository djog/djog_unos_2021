using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6;
    public float maxVelocity = 10;
    
    private Rigidbody2D rb;
    private Vector2 input;

    private Vector2 movementVelocity;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate() {
        movementVelocity = input.normalized * moveSpeed;
        rb.velocity += movementVelocity;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }
}
