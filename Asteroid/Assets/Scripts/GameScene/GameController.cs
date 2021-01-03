using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject asteroid;
    public int asteroidKillCount;
    public Transform shotAsteroid;
    private int waveCounter = 1;
    private int asteroidsForWaveRemaining;
    private GameObject playerObj;
    Collider asteroidCollider;

    //Sun boss battle
    public Transform sunFirePoint1;
    public Transform sunFirePoint2;
    public Transform sunFirePoint3;
    public Transform sunFirePoint4;
    public Transform sunFirePoint5;
    public Transform sunFirePoint6;
    public Transform sunFirePoint7;

    public GameObject sun;
    public GameObject solarFlare;
    private bool asteroidWavesAreOver;
    private int chooseAttack;

    public float flareForce = 20f;


    private int sunHealth = 3; //CHECK BRACKEYS HOW TO MAKE A HEALTH BAR

    void Start()
    {
        sun.SetActive(false);
        asteroidWavesAreOver = false;
        asteroidKillCount = 0;
        asteroidsForWaveRemaining = 10 * waveCounter; //If I can make a function that counts how many asteroids the ship has destroyed, i could then subtract that from asteroidsForWaveRemaining. Once at 0, then we let
        // update call SpawnAsteroids again!

        StartCoroutine(SpawnAsteroids(waveCounter));
        waveCounter++;
    }

    // Update is called once per frame
    void Update()
    {
        //If we have only spawned asteroids for waves 1 and 2 and all the asteroids were destroyed in the
        //current wave, call in the next wave.

        asteroidsForWaveRemaining = asteroidsForWaveRemaining - shotAsteroid.GetComponent<ShotAsteroid>().asteroidsDestroyed;

        Debug.Log("ASTEROIDS LEFT: " + asteroidsForWaveRemaining);


        if (waveCounter <= 4 && GameObject.FindGameObjectsWithTag("Asteroid").Length == 0 && asteroidsForWaveRemaining == 0)
        {
            StartCoroutine(SpawnAsteroids(waveCounter));
            waveCounter++;

            asteroidKillCount = 0;
            asteroidsForWaveRemaining = 10 * waveCounter;

            Debug.Log(waveCounter);
            if (waveCounter > 4)
            {
                asteroidWavesAreOver = true;
            }
        }
        else if (asteroidWavesAreOver && GameObject.FindGameObjectsWithTag("Asteroid").Length == 0)
        {
            //After this is called in update once, it will never be called again
            asteroidWavesAreOver = false;
            sun.SetActive(true);
            StartCoroutine(SunBoss());
        }
    }

    //Spawns asteroids into the Scene
    IEnumerator SpawnAsteroids(int wave)
    {
        ClearAsteroids();

        // Constant
        int asteroidsPerWave = 10;

        //Each wave has 10 more asteroids than the last
        int asteroidsToSpawn = wave * asteroidsPerWave;

        //The following for loop and its asteroid spawning was taken from
        //http://gamecodeschool.com/unity/building-asteroids-arcade-game-in-unity/

        for (int i = 0, h = 6; i < asteroidsToSpawn; i++)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-10.0f, 10.0f),
                    Random.Range(-6.0f, 6.0f), 0);

                // Spawn an asteroid, if it is at the players location do not spawn it.
                Instantiate(asteroid, spawnLocation, Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));

            //Each wave, the spawn size and speed change slightly. This is the block of code that controls
            //this.
            //At Wave 1, spawn 2 asteroids wait 6 seconds, spawn 2 asteroids wait 5 seconds, ...
            //  once h (our time we wait for) reaches 0 it stays at 0 until the following wave
            //Wave 2 spawns 3 asteroids at a time and Wave 3 spawns 4 at a time, keeping the same time
            // pattern.
            if (i % (wave + 1) == 0)
            {
                yield return new WaitForSeconds(h);

                if (h >= 0)
                {
                    h--;
                }
            }

        }

        //Wait 5 seconds between each wave.
        yield return new WaitForSeconds(5);
    }

    //Clears all asteroids in the Scene
    public void ClearAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject current in asteroids)
        GameObject.Destroy(current);
    }


   

    IEnumerator SunBoss()
    {
        while(sunHealth > 0)
        {
            sunHealth = CheckSunHealth();
            SunAttack();
            yield return new WaitForSeconds(2);
        }

        SunDie();
    }

    //Checks the Sun object's health
    int CheckSunHealth()
    {
        return --sunHealth;
    }

    //Chooses a random integer 1-4 and then executes the corresponding attack
    void SunAttack()
    {
        chooseAttack = Random.Range(0, 5);

        //SunDiagonalAttack and SunFragMissile have 2x the chance to be chosen that SunLaserMaze
        // as I don't want the LaserMaze to be overused.
        if (chooseAttack <= 1)
        {
            SunDiagonalAttack();
        }
        else if (chooseAttack <= 3)
        {
            SunFragMissile();
        }
        else
        {
            SunLaserMaze();
        }
    }

    //Shoots 4 solarFlare prefabs left timed such that they form a diagonal line, one of the sunFirePoints will not fire
    // a solarFlare so that the player may dodge the wall of solarFlares
    void SunDiagonalAttack()
    {
        // Create 7 solar flares to be shot out 
        GameObject flare1 = Instantiate(solarFlare, sunFirePoint1.position, solarFlare.transform.rotation);
        GameObject flare2 = Instantiate(solarFlare, sunFirePoint2.position, solarFlare.transform.rotation);
        GameObject flare3 = Instantiate(solarFlare, sunFirePoint3.position, solarFlare.transform.rotation);
        GameObject flare4 = Instantiate(solarFlare, sunFirePoint4.position, solarFlare.transform.rotation);
        GameObject flare5 = Instantiate(solarFlare, sunFirePoint5.position, solarFlare.transform.rotation);
        GameObject flare6 = Instantiate(solarFlare, sunFirePoint6.position, solarFlare.transform.rotation);
        GameObject flare7 = Instantiate(solarFlare, sunFirePoint7.position, solarFlare.transform.rotation);


        // Give the bullets physics
        Rigidbody2D rb1 = flare1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = flare2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = flare3.GetComponent<Rigidbody2D>();
        Rigidbody2D rb4 = flare4.GetComponent<Rigidbody2D>();
        Rigidbody2D rb5 = flare5.GetComponent<Rigidbody2D>();
        Rigidbody2D rb6 = flare6.GetComponent<Rigidbody2D>();
        Rigidbody2D rb7 = flare7.GetComponent<Rigidbody2D>();

        // Add force to the bullets
        rb1.AddForce(sunFirePoint1.transform.TransformDirection(Vector2.left) * flareForce, ForceMode2D.Impulse);
        rb2.AddForce(sunFirePoint2.transform.TransformDirection(Vector2.left) * flareForce, ForceMode2D.Impulse);
        rb3.AddForce(sunFirePoint3.transform.TransformDirection(Vector2.left) * flareForce, ForceMode2D.Impulse);
        rb4.AddForce(sunFirePoint4.transform.TransformDirection(Vector2.left) * flareForce, ForceMode2D.Impulse);
        rb5.AddForce(sunFirePoint5.transform.TransformDirection(Vector2.left) * flareForce, ForceMode2D.Impulse);
        rb6.AddForce(sunFirePoint6.transform.TransformDirection(Vector2.left) * flareForce, ForceMode2D.Impulse);
        rb7.AddForce(sunFirePoint7.transform.TransformDirection(Vector2.left) * flareForce, ForceMode2D.Impulse);

        //Puts all the flares into an array and chooses a random one to delete so that the player has a way to go through
        GameObject[] flares = GameObject.FindGameObjectsWithTag("SolarFlare");
        Destroy(flares[Random.Range(0, 7)]);
    }

    //Shoots 3 large solarFlare that explodes after a set time interval into 4 small solarFlare prefabs which travel up, down, left,
    // and right respectively.
    void SunFragMissile()
    {

    }

    //Produces two lasers that have colliders and trap damage the player on contact, effectively trappning the player in a narrow rectangle. Fires solarFlares
    // forward with single open spots to dodge through.
    void SunLaserMaze()
    {

    }

    //Destroys the Sun object and ends the game
    void SunDie()
    {
        Destroy(sun);
    }
}
