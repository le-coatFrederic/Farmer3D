using System;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private PlayerCameraPosition cameraPosition;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float sprintRatio = 2f;
    [SerializeField] private float rotateSpeed = 20f;
    [SerializeField] private float jumpForce = 2f;

    private bool isWalking = false;
    private bool isRunning = false;

    private Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateMovement();
        this.UpdateRotation();
        this.UpdateAnimation();
    }

    public void UpdateMovement()
    {
        GroundInteraction();
        
        // Get input value
        Vector3 input = this.gameInput.GetMoveVectorNormalized();
        Vector3 moveDir = this.transform.right * input.x + this.transform.forward * input.z;

        // Setting properties
        this.isWalking = moveDir != Vector3.zero;
        this.isRunning = this.gameInput.GetSprintingInput();
        
        if (this.isRunning && this.isWalking)
        {
            this.characterController.Move(moveDir * this.moveSpeed * this.sprintRatio * Time.deltaTime);
        }
        else
        {
            this.characterController.Move(moveDir * this.moveSpeed * Time.deltaTime);
        }
    }

    public void UpdateRotation()
    {
        Vector2 deltaInput = this.gameInput.GetLookVectorNormalized();
        transform.eulerAngles = quaternion.RotateX(deltaInput.x * this.rotateSpeed * Time.deltaTime);
        cameraPosition.SetCameraPosition(deltaInput.y * this.rotateSpeed * Time.deltaTime);
    }

    public void UpdateAnimation() {
        this.animator.walking(this.isWalking);
        this.animator.running(this.isRunning);
    }

    public void GroundInteraction()
    {
        if (this.characterController.isGrounded && this.velocity.y < 0)
        {
            this.velocity.y = 0f;
        }
    }
}


