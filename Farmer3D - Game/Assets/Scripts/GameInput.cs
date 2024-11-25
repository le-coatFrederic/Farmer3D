using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions playerInputAction;

    private void Awake()
    {
        playerInputAction = new InputSystem_Actions();
    }

    public void OnEnable() {
        playerInputAction.Player.Enable();
    }

    public void OnDisable() {
        playerInputAction.Player.Disable();
    }

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector3 GetMoveVector()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        return moveDir;
    }

    public Vector3 GetMoveVectorNormalized()
    {
        Vector3 moveDir = GetMoveVector().normalized;

        return moveDir;
    }

    public Vector2 GetLookVector()
    {
        return playerInputAction.Player.Look.ReadValue<Vector2>();
    }

    public Vector2 GetLookVectorNormalized() 
    {
        return GetLookVector().normalized;
    }

    public bool GetSprintingInput()
    {
        return playerInputAction.Player.Sprint.IsPressed();
    }

    public bool GetJumpingInput() {
        return playerInputAction.Player.Jump.triggered;
    }
}
