using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{
    [Header("Links to my components")]
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private GameObject myShovel;
    [SerializeField] private Animator animator;
    
    [Header("Links to other objects")]
    [SerializeField] private ZombieSpawner[] zombieSpawners;
    
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool eventGameOverIsInvoked = false;
    private bool onExcavationSite = false;
    private ExcavationSite nearbyExcavationSite = null;

    private void OnValidate()
    {
        zombieSpawners = FindObjectsOfType<ZombieSpawner>();
    }

    private void Update()
    {
        // определяем, надо ли двигать персонажа
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;

        // Изменение анимации
        if (moveVelocity != Vector2.zero)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

        // Проверка на поворот персонажа
        if (!facingRight && moveVelocity.x > 0)
        {
            Flip();
        }
        else if (facingRight && moveVelocity.x < 0)
        {
            Flip();
        }

        // Проверка на смерть
        if (hitpoints <= 0 && eventGameOverIsInvoked == false)
        {
            GameplayEvents.eventGameOver.Invoke();
            eventGameOverIsInvoked = true;
        } 

        // Копаем?
        if (nearbyExcavationSite != null)
        {
            if (onExcavationSite && Input.GetKey(KeyCode.F))
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
        foreach (ZombieSpawner zombieSpawner in zombieSpawners)
        {
            zombieSpawner.SetStateIsDigging(value);
        }
    }
}
