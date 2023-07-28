using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 20f;
    
    private Rigidbody rb;
    private Vector3 velocity;
    private Renderer rd;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rd = GetComponent<Renderer>();
        Invoke("Launch", 0.5f);
    }

    void Launch()
    {
        rb.velocity = Vector3.up * speed;
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
        velocity = rb.velocity;
        if (!rd.isVisible)
        {
            GameManager.Instance.Balls--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = Vector3.Reflect(velocity, other.contacts[0].normal);
    }
}
