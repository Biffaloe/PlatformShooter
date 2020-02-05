using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue = 10;

    public bool isDead;
    SphereCollider sphereCollider;

    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        currentHealth = startingHealth;
    }

    public void TakeDamage ( int amount)
    {
        if (isDead)
        {
            ScoreCounter.score += scoreValue;
            Destroy(this.gameObject);
            return;
        }
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        sphereCollider.isTrigger = true;
    }
}
