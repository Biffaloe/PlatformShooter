using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour 
{
	public GameObject bulletPrefab;
	public float timeBetweenBullets = 0.15f;
    public float speed = 2.0f;

    float timer;
	float effectsDisplayTime = 0.2f;
	Light gunLight;

	void Awake ()
	{
		gunLight = GetComponent<Light> ();
	}

	void Update()
	{
		timer += Time.deltaTime;

		if(Input.GetButton("Fire1") && timer >= timeBetweenBullets)
		{
			shootBullet();
		}

		if (timer >= timeBetweenBullets * effectsDisplayTime){
			gunLight.enabled = false;
		}
	}

	public void shootBullet()
	{
		timer = 0f;

		gunLight.enabled = true;

		GameObject b = Instantiate(bulletPrefab) as GameObject;
        Rigidbody rb = b.GetComponent<Rigidbody>();
        b.transform.position = this.transform.position;
        b.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z);

        rb.velocity = transform.up * speed;
    }

    public void DestroyBullet()
    {

    }
}
