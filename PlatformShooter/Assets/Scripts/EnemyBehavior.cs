using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {

    Transform player;
    //PlayerHealth playerhealth;
    //EnemyHealth enemyHealth;
    NavMeshAgent nav;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }
    void Start () {
		
	}
	
	void Update ()
    {
        nav.SetDestination(player.position);
	}
}
