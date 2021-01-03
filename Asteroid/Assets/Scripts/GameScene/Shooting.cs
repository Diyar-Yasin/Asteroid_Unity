using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint1;
    public Transform firePoint2;
    public GameObject bulletPrefab;
    public AudioSource laserSound;
    public GameObject player;

    public float bulletForce = 40f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            laserSound.Play();
            Shoot();
            
        }
    }

    void Shoot()
    {
        //Creates a Quaternion that holds the players current rotation and adds 90 degrees so my bulletPrefab is angled correctly when shot
        Quaternion bulletRotation = Quaternion.identity;
        bulletRotation.eulerAngles = player.transform.rotation.eulerAngles + new Vector3(0, 0, 90);

        // Create bullet1 and 2 game objects and put them into variables
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, bulletRotation);
        GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, bulletRotation);

        // Give the bullets physics
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();

        // Add force to the bullets
        rb1.AddForce(firePoint1.up * bulletForce, ForceMode2D.Impulse);
        rb2.AddForce(firePoint2.up * bulletForce, ForceMode2D.Impulse);
    }
}
