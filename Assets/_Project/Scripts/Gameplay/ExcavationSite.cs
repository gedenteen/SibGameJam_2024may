using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavationSite : MonoBehaviour
{
    [SerializeField] private float initialTime = 15f;
    [SerializeField] private bool isDigging = false;
    [SerializeField] private List<Sprite> sprites= new List<Sprite>();
    [SerializeField] private SpriteRenderer targetSpriteRenderer;
    [SerializeField] private float remainingTime;
    [SerializeField] private float[] timers;
    [SerializeField] private bool isExcavated = false; // раскопано до конца?

    private void Awake()
    {
        remainingTime = initialTime;

        int countOfSprites = sprites.Count;
        timers = new float[countOfSprites];
        for (int i = 0; i < countOfSprites; i++)
        {
            timers[i] = (countOfSprites - i - 1) * initialTime / countOfSprites;
        }

        ExcavationSitesCounter.Instance.Add(this);
    }

    private void FixedUpdate()
    {
        if (remainingTime <= 0 || !isDigging)
        {
            return;
        }

        // Debug.Log($"ExcavationSite: меня копают");

        if (Input.GetKey(KeyCode.LeftControl))
        {
            remainingTime = 0f;
        }
        else
        {
            remainingTime -= Time.fixedDeltaTime;
        }
        TryChangeSprite();

        // Раскопали до конца?
        if (remainingTime <= 0f)
        {
            // Показываем инфу
            WindowWithButtons.eventShowNextData.Invoke();
            isExcavated = true;
            ExcavationSitesCounter.Instance.Remove(this);

            // Если это последняя яма на уровне, тогда показываем последний спрайт
            if (ExcavationSitesCounter.Instance.countOfNotExcavatedSites == 0)
            {
                targetSpriteRenderer.sprite = sprites[sprites.Count - 1];
            }
        }
    }

    private void TryChangeSprite()
    {
        for (int i = 0; i < timers.Length - 1; i++) // АХТУНГ: тут -1
        {
            if (remainingTime >= timers[i])
            {
                targetSpriteRenderer.sprite = sprites[i];
                break;
            }
        }
    }

    public void SetStateIsDigging(bool value)
    {
        isDigging = value;
    }

    public bool CanBeDigged()
    {
        if (remainingTime > 0f)
            return true;
        else
            return false;
    }
}
