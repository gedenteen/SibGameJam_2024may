using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 3f;
    // public int hitpoints = 2;

    protected bool facingRight = true;

    protected void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 eulerAngles = transform.eulerAngles;
        if (facingRight)
        {
            eulerAngles.y = 0f;
        }
        else
        {
            eulerAngles.y = 180f;
        }
        transform.eulerAngles = eulerAngles;
    }
}
