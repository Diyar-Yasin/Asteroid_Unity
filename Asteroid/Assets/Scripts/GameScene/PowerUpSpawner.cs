
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUps;
    private GameObject currentPowerUp;
    private int index;
    private bool startingPlacement;


//Randomely determines which power up to spawn.
    IEnumerator PickPowerUp()
    {
        index = Random.Range(0, powerUps.Length);
        currentPowerUp = powerUps[index];

        yield return new WaitForSeconds(10);
        startingPlacement = true;

        PlacePowerUp(currentPowerUp);
    }

    //Places determined power up onto a random location on the map.
    void PlacePowerUp(GameObject powerUp)
    {
        Instantiate(powerUp, new Vector3(Random.Range(-13.0f, 10.0f), Random.Range(-5.0f, 9.0f), 0),
                Quaternion.Euler(0, 0, 0));
    }

    void Start()
    {
        startingPlacement = false;
        StartCoroutine(PickPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        if (startingPlacement && GameObject.FindGameObjectsWithTag("GunPowerUp").Length == 0
            && GameObject.FindGameObjectsWithTag("NukePowerUp").Length == 0)
        {
            startingPlacement = false;
            StartCoroutine(PickPowerUp());
        } 
    }
}
