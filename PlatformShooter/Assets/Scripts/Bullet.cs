using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int attackDamage;

    float timer;
    float timeLimit = 2.0f;

    void Update() 
	{
        timer += Time.deltaTime;

        if (timer >= timeLimit)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        var enemyHealth = other.GetComponentInParent<EnemyHealth>();
        if (other.tag == "Enemy")
        {
            enemyHealth.TakeDamage(attackDamage);
        }
    }
}
