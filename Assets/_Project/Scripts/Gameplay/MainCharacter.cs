using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private GameObject myShovel;
    
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool eventGameOverIsInvoked = false;
    private bool onExcavationSite = false;
    private ExcavationSite nearbyExcavationSite = null;

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;

        if (hitpoints <= 0 && eventGameOverIsInvoked == false)
        {
            GameplayEvents.eventGameOver.Invoke();
            eventGameOverIsInvoked = true;
        } 

        if (nearbyExcavationSite != null)
        {
            if (onExcavationSite && Input.GetKey(KeyCode.K))
            {
                Dig(true);
            }
            else
            {
                Dig(false);
            }
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckOnExcavationSite(other))
        {
            onExcavationSite = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CheckOnExcavationSite(other))
        {
            onExcavationSite = false;
        }
    }

    private bool CheckOnExcavationSite(Collider2D other)
    {
        ExcavationSite excavationSite = null;
        other.TryGetComponent<ExcavationSite>(out excavationSite);
        
        if (excavationSite != null)
        {
            nearbyExcavationSite = excavationSite;
            return true;
        }

        return false;
    }

    private void Dig(bool value)
    {
        myShovel.SetActive(value);
        nearbyExcavationSite.SetStateIsDigging(value);
    }
}
