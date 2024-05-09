using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool eventGameOverIsInvoked = false;

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;

        if (hitpoints <= 0 && eventGameOverIsInvoked == false)
        {
            GameplayEvents.eventGameOver.Invoke();
            eventGameOverIsInvoked = true;
        } 
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + moveVelocity * Time.fixedDeltaTime);
    }
}
