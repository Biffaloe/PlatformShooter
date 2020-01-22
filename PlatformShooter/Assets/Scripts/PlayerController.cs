using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    private Vector3 movement;
    public float Speed; 
    public float GravityScale;
    public new Camera camera;
    public float jumpForce;
    public LayerMask layer;
    CameraControllerPlatfroming cameraController;

    private bool isGrounded;
    private float hitDistance;

    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), movement.y, Input.GetAxis("Vertical"));
    }
    private void FixedUpdate()
    {
        moveCharacter(movement);
        UpdateStats();
        Jumps();
    }

    public void UpdateStats()
    {
        hitDistance = isGrounded ? 0.45f : 0.35f;
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, hitDistance, layer);
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
        this.GetComponent<Rigidbody>().MovePosition((Vector3)transform.position + (desiredMoveDirection * Speed * Time.deltaTime));
    }

    void Jumps()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (isGrounded)
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

    }
}