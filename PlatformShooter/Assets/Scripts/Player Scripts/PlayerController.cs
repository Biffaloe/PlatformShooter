using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    private Vector3 movement;
    private bool canJump;

    public float Speed; 
    public float GravityScale;
    public float jumpForce;
    public new Camera camera;
    public LayerMask layer;
    
    private bool isGrounded;
    private float hitDistance;

    CameraControllerPlatfroming cameraController;
    GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        cameraController = mainCamera.GetComponent<CameraControllerPlatfroming>();
    }
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
        if (Input.GetButtonDown("Jump"))
            if (isGrounded)
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
            }
    }
 
    public void UpdateStats()
    {
       isGrounded = Physics.Raycast(transform.position, Vector3.down, hitDistance, layer);
       hitDistance = isGrounded ? 0.40f : 0.35f;
    }

    void LockonJumps()
    {
        if (cameraController.lockedOn)
        {
            if (isGrounded)
            {
                if (Input.GetButtonDown("Jump") && Input.GetKeyDown(KeyCode.A))
                    this.GetComponent<Rigidbody>().AddForce(new Vector3((jumpForce), jumpForce - (jumpForce - 3), 0), ForceMode.VelocityChange);
                if (Input.GetButtonDown("Jump") && Input.GetKeyDown(KeyCode.D))
                    this.GetComponent<Rigidbody>().AddForce(new Vector3(-(jumpForce), jumpForce - (jumpForce - 3), 0), ForceMode.VelocityChange);
            }
        }
    }
}