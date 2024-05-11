using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Character
{
    // public float speed = 7f;
    public int maxHealth = 5;
    public int currentHealth;
    private bool isDead = false;
    public int damage = 1;

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
    private WeaponParent weaponParent;
    private GunParent gunParent;

    private void OnValidate()
    {
        zombieSpawners = FindObjectsOfType<ZombieSpawner>();
    }


    private void Start()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
        gunParent = GetComponentInChildren<GunParent>();
        currentHealth = maxHealth;
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
        if (currentHealth <= 0 && eventGameOverIsInvoked == false)
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
        moveVelocity = moveInput * speed;    

        if (Input.GetKeyDown(KeyCode.Space))
            this.Attack();
        if (Input.GetMouseButtonDown(1))
            this.Shoot();
        if (Input.GetKeyDown(KeyCode.R))
            this.Reload();
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

    private void Attack()   { weaponParent.Attack(); }

    private void Shoot()    { gunParent.Shoot(); }

    private void Reload() { gunParent.Reload(); }
}
