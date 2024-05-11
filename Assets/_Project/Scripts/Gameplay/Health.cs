using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2d;
    public float knockbackForce = 10f; // Сила отброса

    [Header("Changeable")]
    public int currentHealt = 5;
    public int maxHealth = 5;
    public bool regen = false;
    public float delayBeforeRegen = 5f;

    [SerializeField]
    private bool isDead;

    public UnityEvent<GameObject> OnHitReference, OnDeathWithReference;

    private float timerForRegen = 0f;

    private void Awake()
    {
        timerForRegen = delayBeforeRegen;
    }

    public void Update()
    {
        if (regen)
        {
            if (timerForRegen > 0f)
            {
                timerForRegen -= Time.deltaTime;
            }
            else if (timerForRegen <= 0f)
            {
                currentHealt = Math.Clamp(currentHealt + 1, 1, maxHealth);
                timerForRegen = delayBeforeRegen;
            }
        }
    }

    public void InitializeHealth(int health)
    {
        currentHealt = health;
        // maxHealth = health;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        Debug.Log($"Health: GetHit: sender.name={sender.name} gameObject.name={gameObject.name}");
        Debug.Log($"Health: GetHit: sender.position={sender.transform.position} gameObject.position={gameObject.transform.position}");

        currentHealt -= amount;

        if (currentHealt > 0)
        {
            OnHitReference?.Invoke(sender);

            // Применяем отброс персонажа от позиции отправителя
            if (rigidbody2d != null)
            {
                Vector2 direction = (Vector2)(transform.position - sender.transform.position);
                direction.Normalize();
                rigidbody2d.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogError($"Health: GetHit: нет ссылки на rigidbody (gameObject.name={gameObject.name})");
            }
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            //Destroy(gameObject);
        }
    }

}
