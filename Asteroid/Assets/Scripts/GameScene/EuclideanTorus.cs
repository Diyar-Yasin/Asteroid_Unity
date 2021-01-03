using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuclideanTorus : MonoBehaviour
{
    //This code was taken from
    //http://gamecodeschool.com/unity/building-asteroids-arcade-game-in-unity/

    // Update is called once per frame
    void Update()
    {
        // Teleport the game object
        if (transform.position.x > 12)
        {

            transform.position = new Vector3(-15, transform.position.y, 0);

        }
        else if (transform.position.x < -15)
        {
            transform.position = new Vector3(12, transform.position.y, 0);
        }

        else if (transform.position.y > 11)
        {
            transform.position = new Vector3(transform.position.x, -6, 0);
        }

        else if (transform.position.y < -6)
        {
            transform.position = new Vector3(transform.position.x, 11, 0);
        }
    }
}
