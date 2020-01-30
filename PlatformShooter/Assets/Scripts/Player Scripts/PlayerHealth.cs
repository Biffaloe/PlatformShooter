using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    PlayerController playerController;

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


        if(currentHealth <=0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        var playerController = this.GetComponentInParent<PlayerController>();
        var meshrender = this.GetComponentInParent<MeshRenderer>();

        meshrender.enabled = false;
        playerController.enabled = false;
    }
}
