using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSmallAsteroid : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            Destroy(gameObject);
        }
    }
}
