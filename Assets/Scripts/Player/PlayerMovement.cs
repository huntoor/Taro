using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Vector2 movementDirection;

    private PlayerInputActions playerInputActions;
    private Rigidbody2D myRigidBody;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        playerInputActions = new PlayerInputActions();

        playerInputActions.Movement.Enable();
        playerInputActions.Movement.Swim.performed += OnSwimInput;
        playerInputActions.Movement.Swim.canceled += OnSwimInput;
    }

    private void Start()
    {
        speed = 20f;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnSwimInput(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        Vector2 targetVelocity = movementDirection * speed;
        
        myRigidBody.velocity = targetVelocity;
    }
}
