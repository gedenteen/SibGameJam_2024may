using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextIntro : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> list = new List<TextMeshProUGUI>();

    int currentIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (currentIndex < list.Count - 1)
            {
                list[currentIndex].gameObject.SetActive(false);
                currentIndex++;
                list[currentIndex].gameObject.SetActive(true);
            }
            else if (currentIndex == list.Count - 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
