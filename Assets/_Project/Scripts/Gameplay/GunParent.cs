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
    private float fireRate = 0f;
    private float reloadRate = 0f;

    private Vector2 direction;
    private int bulletMaxCount = 5;
    private int bulletCurrentCount;

    void Start()
    {
        bulletCurrentCount = bulletMaxCount;
    }

    void Update()
    {
        // �������� ���������� ������� � �������� ������������ ������ �������� ���� ������
        Vector3 cursorPositionPixels = Input.mousePosition;

        // �������� ����� ������ � ��������
        Vector3 screenCenterPixels = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        // ��������� �������� ������� ������������ ������ ������
        Vector3 cursorOffsetFromCenter = cursorPositionPixels - screenCenterPixels;

        // ����������� ������� � ���������� ������ (-1, -1) - ������ ����� ����, (1, 1) - ������� ������ ����
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
    }

    public void Shoot()    
    {
        float timerOut = Time.deltaTime * 10;
        if (bulletCurrentCount > 0 && reloadRate <= 0f)
        {
            if (fireRate <= 0f)
            {
                // ��������� ����������� � ���������� ����� startPos � endPos
                Vector3 direction = (firingPoint.position - startPoint.position).normalized;
                float distance = Vector3.Distance(startPoint.position, firingPoint.position);

                // ������� ���� � ������ ����� �����������
                GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
                // ������� ���� � ����������� endPos
                bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f;


                //Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(0, 0, angle));
                bulletCurrentCount--;
                fireRate = 0.02f;
            }
            else
            {
                fireRate -= timerOut;
            }
        }
        else 
        { 
            reloadRate -= timerOut;
        }   
    }

    public void Reload()
    {
        bulletCurrentCount = bulletMaxCount;
        reloadRate = 0.02f;
        animator.SetTrigger("Reload");
    }
}
