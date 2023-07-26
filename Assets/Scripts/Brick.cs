using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hits;
    public int points;
    public Vector3 rotator;
    public Material hitMaterial;

    private Material orgMaterial;
    private Renderer renderer;

    void Start()
    {
        transform.Rotate(rotator * (transform.position.x + transform.position.y) * 0.1f);
        renderer = GetComponent<Renderer>();
        orgMaterial = renderer.sharedMaterial;
    }

    void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        hits--;
        if (hits <= 0)
        {
            GameManager.Instance.Score += points;
            Destroy(gameObject);
        }
        renderer.sharedMaterial = hitMaterial;
        Invoke("RestoreMaterial",0.05f);
    }

    private void RestoreMaterial()
    {
        renderer.sharedMaterial = orgMaterial;
    }
}
