using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForGround : MonoBehaviour {
    public Rigidbody RB;
    public float jumpForce;
    public bool isGrounded;
    public float hitDistance;
    public LayerMask layer;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        UpdateStats();

        if (Input.GetKeyDown(KeyCode.Space))
            if (isGrounded)
                RB.AddForce(new Vector3(0, jumpForce, 0));
    }

    public void UpdateStats()
    {
        hitDistance = isGrounded ? 0.45f : 0.35f;
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, hitDistance, layer);
    }
}
