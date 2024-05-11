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
    private int bulletMaxCount = 7;
    private int bulletCurrentCount;

    void Start()
    {
        bulletCurrentCount = bulletMaxCount;
    }

    void Update()
    {
        // Получаем координаты курсора в пикселях относительно левого верхнего угла экрана
        Vector3 cursorPositionPixels = Input.mousePosition;

        // Получаем центр экрана в пикселях
        Vector3 screenCenterPixels = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        // Вычисляем смещение курсора относительно центра экрана
        Vector3 cursorOffsetFromCenter = cursorPositionPixels - screenCenterPixels;

        // Преобразуем пиксели в координаты экрана (-1, -1) - нижний левый угол, (1, 1) - верхний правый угол
        Vector3 cursorPositionNormalized = new Vector3(
            cursorOffsetFromCenter.x / (Screen.width / 2f),
            cursorOffsetFromCenter.y / (Screen.height / 2f),
            0
        );

        Vector2 direction = cursorPositionNormalized;

        transform.right = direction;

        //Vector2 scale = transform.localScale;
        //Vector3 firePointScale = firingPoint.localScale;
        //if (direction.x < 0)
        //{
        //    scale.y = -1;
        //    firePointScale.y *= -1;
        //    firePointScale.x *= -1;
        //    firingPoint.rotation.Set(firingPoint.rotation.x, firingPoint.rotation.y, firingPoint.rotation.z-300, firingPoint.rotation.w);
        //}    
        //else if (direction.x > 0)
        //{
        //    scale.y = 1;
        //    firePointScale.y *= 1;
        //    firePointScale.x *= 1;
        //    firingPoint.rotation.Set(firingPoint.rotation.x, firingPoint.rotation.y, firingPoint.rotation.z + 300, firingPoint.rotation.w);
        //}
        //transform.localScale = scale;
        //firingPoint.localScale.Set(firePointScale.x, firePointScale.y, firePointScale.z);

        //Vector3 mouse_pos = Input.mousePosition;
        //mouse_pos.z = -20;
        //Vector3 object_pos = Camera.main.WorldToScreenPoint(firingPoint.position);
        //Quaternion mouse = Quaternion.Euler(0, 0, Mathf.Atan2(mouse_pos.y - object_pos.y, mouse_pos.x - object_pos.x) * Mathf.Rad2Deg);
        //transform.rotation = mouse;
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
