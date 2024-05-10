using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealt, maxHealth;

    [SerializeField]
    private bool isDead;

    public UnityEvent<GameObject> OnHitReference, OnDeathWithReference;


    public void InitializeHealth(int health)
    {
        currentHealt = health;
        maxHealth = health;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;
        Debug.Log(gameObject.name);
        currentHealt -= amount;

        if (currentHealt > 0)
            OnHitReference?.Invoke(sender);
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            //Destroy(gameObject);
        }
    }

}
