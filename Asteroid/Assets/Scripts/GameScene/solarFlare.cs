using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solarFlare : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
