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

    private void Awake()
    {
        remainingTime = initialTime;

        int countOfSprites = sprites.Count;
        timers = new float[countOfSprites];
        for (int i = 0; i < countOfSprites; i++)
        {
            timers[i] = (countOfSprites - i - 1) * initialTime / countOfSprites;
        }
    }

    private void FixedUpdate()
    {
        if (remainingTime <= 0 || !isDigging)
        {
            return;
        }

        // Debug.Log($"ExcavationSite: меня копают");

        remainingTime -= Time.fixedDeltaTime;
        TryChangeSprite();

        // Раскопали до конца?
        if (remainingTime <= 0f)
        {
            // Показываем инфу
            WindowWithButtons.eventShowNextData.Invoke();
        }
    }

    private void TryChangeSprite()
    {
        for (int i = 0; i < timers.Length; i++)
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
}
