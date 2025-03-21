using System;
using UnityEngine;

public class Player : BeyBladeBehaviour
{
    private Controls playerControl;


    protected override void Start()
    {
        base.Start();
    }

    private void Awake()
    {
        playerControl = new Controls();
    }

    private void FixedUpdate()
    {
        // Read movement input
        Vector2 movementInput = playerControl.Player.Move.ReadValue<Vector2>();
        
        Debug.Log("Move: "+ movementInput);
        
        // Convert to 3D space (assuming XZ plane movement)
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
        
        // Apply movement force
        if (movement.magnitude > 0.1f)
        {
            Rb.AddForce(movement * MoveSpeed, ForceMode.Force);
            
            // Limit maximum velocity
            if (Rb.linearVelocity.magnitude > MaxMoveVelocity)
            {
                Rb.linearVelocity = Rb.linearVelocity.normalized * MaxMoveVelocity;
            }
        }
    }

    private void OnEnable()
    {
        playerControl.Player.Enable();
    }

    private void OnDisable()
    {
        playerControl.Player.Disable();
    }
}
