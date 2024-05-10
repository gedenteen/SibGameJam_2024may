using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public float speed = 7f;
    public int maxHealth = 5;
    public int currentHealth;
    private bool isDead = false;
    public int damage = 1;

    [SerializeField] private Rigidbody2D rigidbody2D;
    
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool eventGameOverIsInvoked = false;
    private WeaponParent weaponParent;


    private void Start()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
        currentHealth = maxHealth;
    }
    private void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;

        if (currentHealth <= 0 && eventGameOverIsInvoked == false)
        {
            GameplayEvents.eventGameOver.Invoke();
            eventGameOverIsInvoked = true;
        }
        moveVelocity = moveInput * speed;    

        if (Input.GetKeyDown(KeyCode.Space))
            this.Attack();
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Attack()
    {
        weaponParent.Attack();
    }
}
