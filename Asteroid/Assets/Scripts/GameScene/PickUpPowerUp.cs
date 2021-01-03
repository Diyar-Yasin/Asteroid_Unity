using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPowerUp : MonoBehaviour
{
    public Transform firePoint3;
    public Transform firePoint4;
    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject pickUpEffectRingSmall;
    public GameObject pickUpEffectRingMedium;
    public GameObject pickUpEffectRingLarge;
    public AudioSource pickUpSound;
    public AudioSource nukeSound;

    GameController _GameController;

    public float bulletForce = 20f;

    bool doWeHaveGunPowerUp;
    bool doWeHaveNukePowerUp;
    Coroutine cor;
    private float timer;

    private SpriteRenderer spRenderer;
    public Sprite sprite1;
    public Sprite sprite2;

    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        if (spRenderer.sprite == null)
        {
            spRenderer.sprite = sprite1;
        }

        doWeHaveGunPowerUp = false;
        timer = 10.0f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GunPowerUp"))
        {
            GunPickup(collision); 
        }
        if (collision.CompareTag("NukePowerUp"))
        {
            NukePickup(collision);
        }

    }

    void NukePickup(Collider2D collision)
    {
        GameObject ringS = Instantiate(pickUpEffectRingSmall, transform.position, transform.rotation);
        GameObject ringM = Instantiate(pickUpEffectRingMedium, transform.position, transform.rotation);
        GameObject ringL = Instantiate(pickUpEffectRingLarge, transform.position, transform.rotation);
        Destroy(ringS, 0.1f);
        Destroy(ringM, 0.2f);
        Destroy(ringL, 0.3f);

        nukeSound.Play();
        StartCoroutine(NukePowerUp());
        Destroy(collision.GetComponent<Collider2D>().gameObject);
    }
    
    IEnumerator NukePowerUp()
    {
        yield return new WaitForSeconds(3);
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject current in asteroids)
        GameObject.Destroy(current);
    }

    void GunPickup(Collider2D collision)
    {
        spRenderer.sprite = sprite2;
        GameObject ringS = Instantiate(pickUpEffectRingSmall, transform.position, transform.rotation);
        GameObject ringM = Instantiate(pickUpEffectRingMedium, transform.position, transform.rotation);
        GameObject ringL = Instantiate(pickUpEffectRingLarge, transform.position, transform.rotation);
        Destroy(ringS, 0.1f);
        Destroy(ringM, 0.2f);
        Destroy(ringL, 0.3f);

        pickUpSound.Play();

        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        cor = StartCoroutine(GunPowerUp());

        Destroy(collision.GetComponent<Collider2D>().gameObject);
    }

    //Determines what boosts the Player receives after colliding with the GunPowerUp
    IEnumerator GunPowerUp()
    {
        doWeHaveGunPowerUp = true;
        yield return new WaitForSeconds(timer);
        spRenderer.sprite = sprite1;
        doWeHaveGunPowerUp = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && doWeHaveGunPowerUp)
        {
            ShootExtra();
        }
    }

    void ShootExtra()
    {
        //Creates a Quaternion that holds the players current rotation and adds 90 degrees so my bulletPrefab is angled correctly when shot
        Quaternion bulletRotation = Quaternion.identity;
        bulletRotation.eulerAngles = player.transform.rotation.eulerAngles + new Vector3(0, 0, 90);

        // Create bullet1 and 2 game objects and put them into variables
        GameObject bullet3 = Instantiate(bulletPrefab, firePoint3.position, bulletRotation);
        GameObject bullet4 = Instantiate(bulletPrefab, firePoint4.position, bulletRotation);

        // Give the bullets physics
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
        Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();

        // Add force to the bullets
        rb3.AddForce(firePoint3.up * bulletForce, ForceMode2D.Impulse);
        rb4.AddForce(firePoint4.up * bulletForce, ForceMode2D.Impulse);
    }
}
