using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 20f;
    
    private Rigidbody rb;
    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.down * speed;
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
        velocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = Vector3.Reflect(velocity, other.contacts[0].normal);
    }
}
