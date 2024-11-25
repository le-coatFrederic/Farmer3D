using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float sprintRatio = 2f;
    [SerializeField] private float rotateSpeed = 2f;
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
        this.UpdateAnimation();
    }

    public void UpdateMovement()
    {
        if (this.characterController.isGrounded && this.velocity.y < 0)
        {
            this.velocity.y = 0f;
        }
        
        // Get input value
        Vector3 moveDir = this.gameInput.GetMoveVectorNormalized();

        // Setting properties
        this.isWalking = moveDir != Vector3.zero;
        this.isRunning = this.gameInput.GetSprintingInput();

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
        
        velocity.y += Physics.gravity.y * Time.deltaTime;
        moveDir = transform.TransformDirection(moveDir);
        
        if (this.isWalking)
        {
            this.transform.forward = Vector3.Slerp(this.transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        }
        
        if (this.isRunning)
        {
            this.characterController.Move(moveDir * this.moveSpeed * this.sprintRatio * Time.deltaTime);
        }
        else
        {
            this.characterController.Move(moveDir * this.moveSpeed * Time.deltaTime);
        }
    }

    public void UpdateAnimation() {
        this.animator.walking(this.isWalking);
        this.animator.running(this.isRunning);
    }
}


