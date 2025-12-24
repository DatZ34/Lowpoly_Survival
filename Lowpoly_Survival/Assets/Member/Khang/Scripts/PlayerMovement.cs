using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerMovement : HumanoiAnimationController
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float sprintSpeed = 4f;
    public float runSpeed = 6f;
    public float jumpForce = 1f;
    public float gravity = -20f;
    public float rotationSpeed = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;    

    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 velocity;
    private bool isGrounded;

    // Animation Blending
    private float currentVelX, currentVelZ;
    private float velXSmooth, velZSmooth;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<CharacterController>();
        if (Camera.main != null) cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        CheckGround();
        HandleMovement();
        HandleJump();
        HandleAnimationBlend();
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep the player grounded
        }
    }  
    
    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 camFwd = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camFwd.y = 0; camRight.y = 0;
        camFwd.Normalize(); camRight.Normalize();

        Vector3 moveDir = camFwd * z + camRight * x;
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : runSpeed;

        if (moveDir.magnitude > 0.1f)
        {
            controller.Move(moveDir * speed * Time.deltaTime);
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }    

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            TriggerJump();
        }
    }   
    
    void HandleAnimationBlend()
    {
        float targetX = Input.GetAxis("Horizontal");
        float targetZ = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetX *= 2;
            targetZ *= 2;
        }

        currentVelX = Mathf.SmoothDamp(currentVelX, targetX, ref velXSmooth, 0.1f);
        currentVelZ = Mathf.SmoothDamp(currentVelZ, targetZ, ref velZSmooth, 0.1f);

        SetVelocity(currentVelX, currentVelZ);
        SetGrounded(isGrounded);
        SetSpeed(new Vector2(currentVelX, currentVelZ).magnitude);
    }    
}