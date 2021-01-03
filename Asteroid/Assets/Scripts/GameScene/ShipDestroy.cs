using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShipDestroy : MonoBehaviour
{
    public AudioSource deathSound;
    public GameObject deathEffect;
    public AudioSource hurtSound;

    private int lives = 50;
    public TMP_Text livesText;

    private bool invincible;
    private float invincibilityTime = 2f;

    private SpriteRenderer spRenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    private bool isPoweredUp;

    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        if (spRenderer.sprite == null)
        {
            spRenderer.sprite = sprite1;
        }
        isPoweredUp = false;
        invincible = false;
        livesText.text = "LIVES : " + (lives - 1);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.tag == "Asteroid")
            {
                StartCoroutine(Invincibility());
            }
            
        }
    } 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible)
        {
            if (collision.gameObject.tag == "SolarFlare")
            {
                Destroy(collision.gameObject);
                StartCoroutine(Invincibility());
            }
        }
    }

    IEnumerator Invincibility()
    {
        if (lives > 1)
        {
            invincible = true;
            hurtSound.Play();
            
            if (spRenderer.sprite == sprite3)
            {
                isPoweredUp = true;
            }
            spRenderer.sprite = sprite2;
            lives--;
            livesText.text = "LIVES : " + (lives - 1);

            yield return new WaitForSeconds(invincibilityTime);
            invincible = false;

            if (isPoweredUp)
            {
                spRenderer.sprite = sprite3;
            }
            else
            {
                spRenderer.sprite = sprite1;
            }
        }
        else
        {
            //PROBLEM: If I remove StartCoroutine() this code works, however then the main menu
            //loads up instantly. I would like to give some time in between. 
            ReturnToMenu();
        }
    }

    void ReturnToMenu()
    {
        deathSound.Play();
        GameObject death = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(death, 1f);
        Destroy(gameObject);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }





}
