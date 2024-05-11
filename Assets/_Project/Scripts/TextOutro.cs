using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TextOutro : MonoBehaviour
{
    public static UnityEvent eventActivate = new UnityEvent();
    [SerializeField] private List<TextMeshProUGUI> list = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject content;

    int currentIndex = 0;
    bool activated = false;

    private void Awake()
    {
        eventActivate.AddListener(Activate);
    }

    private void Activate()
    {
        content.SetActive(true);
        activated = true;
    }

    private void Update()
    {
        if (activated && Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (currentIndex < list.Count - 1)
            {
                list[currentIndex].gameObject.SetActive(false);
                currentIndex++;
                list[currentIndex].gameObject.SetActive(true);
            }
        }
    }
}
