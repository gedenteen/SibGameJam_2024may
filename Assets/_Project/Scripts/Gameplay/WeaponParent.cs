using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer charRender, weaponRender;

    //private InputActionReference pointerPosition;
    public Vector2 PointerPosition {  get; set; }
    public Vector2 mousePos;
    public Animator animator;
    public float delay = 0.3f;

    public Transform circleOrig;
    public float radius;

    private bool isAttack;

    void Update()
    {
        Vector3 mouseposition = Input.mousePosition;
        //mouseposition.z = Camera.main.nearClipPlane;
        //mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);
        //Debug.Log("Mouse:" + mouseposition);
        //Debug.Log("Player:" + (Vector2)transform.position);

        //Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        //mousePos.z = Camera.main.nearClipPlane;
        //Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = (mouseposition).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
            scale.y = -1;
        else if (direction.x > 0)
            scale.y = 1;
        transform.localScale = scale;

        //if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        //    weaponRender.sortingOrder = charRender.sortingOrder - 1;
        //else
        //    weaponRender.sortingOrder = charRender.sortingOrder + 1;
    }

    public void Attack()
    {
        if (isAttack) 
            return;
        animator.SetTrigger("Attack");
        isAttack = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        isAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 position = circleOrig == null ? Vector3.zero : circleOrig.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectCollider()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrig.position, radius))
        {
            //Debug.Log(collider.name);
            Health health;
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(1, transform.parent.gameObject);
            }
        }
    }
}
