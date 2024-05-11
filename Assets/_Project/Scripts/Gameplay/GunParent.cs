using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParent : MonoBehaviour
{
    public Transform Gun;
    public Animator animator;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    //[Range(0.1df, 1f)]
    //[SerializeField] private float firRate = 0.5f;

    private Vector2 direction;
    private Vector2 mousePosition;
    private int bulletMaxCount = 3;
    private int bulletCurrentCount;

    void Start()
    {
        bulletCurrentCount = bulletMaxCount;
    }

    void Update()
    {
        Vector3 cursorPositionPixels = Input.mousePosition;
        Vector3 screenCenterPixels = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        Vector3 cursorOffsetFromCenter = cursorPositionPixels - screenCenterPixels;
        Vector3 cursorPositionNormalized = new Vector3(
            cursorOffsetFromCenter.x / (Screen.width / 2f),
            cursorOffsetFromCenter.y / (Screen.height / 2f),
            0
        );
        float angle = Mathf.Atan2(cursorOffsetFromCenter.y - transform.position.y, cursorOffsetFromCenter.x - transform.position.x) * Mathf.Rad2Deg - 40f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    public void Shoot()
    {
        if (bulletCurrentCount > 0)
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            bulletCurrentCount--;
        }   
    }

    public void Reload()
    {
        bulletCurrentCount = bulletMaxCount;
        animator.SetTrigger("Reload");
    }
}
