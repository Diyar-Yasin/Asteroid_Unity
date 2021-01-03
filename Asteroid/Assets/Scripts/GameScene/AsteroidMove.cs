using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float asteroidSpeed = 5f;

    // Takes min and max float values and outputs a random Vector3 with each
    // parameter within that range.
    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        var z = Random.Range(min, max);
        return new Vector3(x, y, z);
    }

    void Start()
    {
        // Creation of a random direction for asteroid to follow
        rb.velocity = RandomVector(-5f, 5f);
    }


// Update is called once per frame
    void FixedUpdate()
    {
        // Movement of asteroid 
        rb.MovePosition(rb.position + rb.velocity * asteroidSpeed * Time.fixedDeltaTime);
    }

/*    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }*/
}