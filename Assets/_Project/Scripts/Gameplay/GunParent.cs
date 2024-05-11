using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GunParent : MonoBehaviour
{
    public Transform Gun;
    public Animator animator;
    [SerializeField] public Transform startPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [SerializeField] private AudioClip shootSound;

    private float fireRate = 0f;
    private float reloadRate = 0f;

    private Vector2 direction;
    private int bulletMaxCount = 3;
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

        //Debug.Log("Cursor position offset from center: " + cursorPositionNormalized);
        Vector2 direction = cursorPositionNormalized;

        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
            scale.y = -1;
        else if (direction.x > 0)
            scale.y = 1;
        transform.localScale = scale;

        float timerOut = Time.deltaTime;
        fireRate -= timerOut;
        reloadRate -= timerOut;
    }

    public void Shoot()    
    {
        if (bulletCurrentCount > 0 && reloadRate <= 0f)
        {
            if (fireRate <= 0f)
            {
                // Вычисляем направление и расстояние между startPos и endPos
                Vector3 direction = (firingPoint.position - startPoint.position).normalized;
                float distance = Vector3.Distance(startPoint.position, firingPoint.position);

                // Создаем пулю с учетом этого направления
                GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
                // Двигаем пулю в направлении endPos
                bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f;


                SoundManager.instance.PlaySound(shootSound, transform, 0.5f);

                //Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(0, 0, angle));
                bulletCurrentCount--;
                fireRate = 0.2f;
            }
        }  
    }

    public void Reload()
    {
        bulletCurrentCount = bulletMaxCount;
        reloadRate = 0.6f;
        animator.SetTrigger("Reload");
    }
}
