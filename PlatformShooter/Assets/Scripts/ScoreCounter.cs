using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public Text myText;
    private int score;

	void Start ()
    {
        score = 0;
        myText.text = "Score: 0";
	}
	

	void Update ()
    {
        var enemyHealth = this.GetComponentInParent<EnemyHealth>();

        if (enemyHealth.isDead)
            score += 10;

        myText.text = "Score: " + score;
    }
}
