using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    bool isGrounded = true;
    public Rigidbody RB;
    private Vector3 movement;
    private bool canJump;
    public float Speed; 
    public float jumpForce;
    public float GravityScale;
    public new Camera camera;


	void Start ()
    {
        RB = GetComponent<Rigidbody>();
	}
    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), movement.y, Input.GetAxis("Vertical"));
        if(isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                RB.velocity = new Vector3(movement.x, jumpForce, movement.z);
            }
        }
    }
    private void FixedUpdate()
    {
        moveCharacter(movement);
    }
    void moveCharacter(Vector3 direction)
    {
        //reading the input:
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

        //now we can apply the movement:
        RB.MovePosition((Vector3)transform.position + (desiredMoveDirection * Speed * Time.deltaTime));
    }
}