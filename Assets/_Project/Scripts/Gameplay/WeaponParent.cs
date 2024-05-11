using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    [Header("Changeable params")]
    [SerializeField] private bool canRotateWeapon = false;
    [SerializeField] private float delayBeforeAttack = 0.2f;
    [SerializeField] private float delayBeforeFinishAttack = 0.3f;
    public float radius = 0.91f;

    [Header("Links to objects")]
    public SpriteRenderer charRender, weaponRender;
    public Animator animator;
    public Transform circleOrig;

    [Header("Links to objects")]
    public Vector2 mousePos;
    public Vector2 PointerPosition {  get; set; }
    public float offset;

    private bool isAttack;
    private MainCharacter mainCharacter;

    private void Start()
    {
        mainCharacter = FindObjectOfType<MainCharacter>();
    }

    void Update()
    {
        if (isAttack && canRotateWeapon)
        {
            RotateWeapon();
        }
    }

    private void RotateWeapon()
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

        ////Debug.Log("Cursor position offset from center: " + cursorPositionNormalized);
        Vector2 direction = cursorPositionNormalized;

        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
            scale.y = -1;
        else if (direction.x > 0)
            scale.y = 1;
        transform.localScale = scale;
    }

    public void TryAttack()
    {
        if (isAttack) 
            return;
        isAttack = true;

        StartCoroutine(DoAttackWithDelay(delayBeforeAttack));
    }

    private IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(delayBeforeFinishAttack);
        isAttack = false;
        if (weaponRender != null)
        {
            weaponRender.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 position = circleOrig == null ? Vector3.zero : circleOrig.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    private IEnumerator DoAttackWithDelay(float delay)
    {
        if (delay != 0f)
        {
            yield return new WaitForSeconds(delay);
        }

        if (weaponRender != null)
        {
            weaponRender.enabled = true;
        }
        animator.SetTrigger("Attack");
        DetectCollider();
        StartCoroutine(FinishAttack());
    }

    public void DetectCollider()
    {
        Debug.Log($"WeaponParent: DetectCollider: begin");
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrig.position, radius))
        {
            Debug.Log($"WeaponParent: DetectCollider: collider.name={collider.name}");
            Health health;
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(1, transform.parent.gameObject);
            }
            Debug.Log($"WeaponParent: DetectCollider: health={health}");
        }
    }
}
