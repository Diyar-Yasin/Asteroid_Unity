using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAsteroid : MonoBehaviour
{
    public GameObject smallAsteroidPrefab;
    public Transform gameController;
    public int asteroidsDestroyed;

    void Start()
    {
        asteroidsDestroyed = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            asteroidsDestroyed = gameController.GetComponent<GameController>().asteroidKillCount + 1;
            Debug.Log("ASTEROIDS DESTROYED: " + asteroidsDestroyed);
            /*if (gameController.asteroidKillCount == null)
            {
                gameController.asteroidKillCount = 1;
            }
            else
            {
                gameController.asteroidKillCount += 1;
            }*/
            
            Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
            //GameObject pieces2 = Instantiate(smallAsteroidPrefab, transform.position + 2.0f, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
