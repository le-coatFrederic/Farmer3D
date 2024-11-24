using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float sprintRatio = 2f;
    [SerializeField] private float rotateSpeed;

    private bool isWalking = false;
    private bool isRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
    }

    public void Move()
    {
        // Get input value
        Vector3 moveDir = this.gameInput.GetMoveVectorNormalized();

        // Setting properties
        this.isWalking = moveDir != Vector3.zero;
        this.isRunning = this.gameInput.GetSprintingValue();

        // Animation
        this.animator.SetBool("IsMoving", this.isWalking);
        this.animator.SetBool("IsRunning", this.isRunning);
        
        if (this.isRunning)
        {
            this.characterController.Move(moveDir * this.moveSpeed * this.sprintRatio * Time.deltaTime);
        }
        else
        {
            this.characterController.Move(moveDir * this.moveSpeed * Time.deltaTime);
        }

        // Rotation following movement
        this.transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * this.rotateSpeed);
    }
}
