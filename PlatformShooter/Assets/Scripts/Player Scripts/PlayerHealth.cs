using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public AudioClip deathClip;

    //AudioSource playerAudio;
    PlayerController playerController;
    CameraControllerPlatfroming cameraControllerPlatfroming;

    bool isDead;
    bool damaged;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        currentHealth = startingHealth;
        isDead = false;
    }

    public void TakeDamage (int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        //playerAudio.Play();

        if(currentHealth <=0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        playerController.enabled = false;
        cameraControllerPlatfroming.enabled = false;
    }
}
