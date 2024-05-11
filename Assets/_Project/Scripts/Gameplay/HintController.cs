using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HintController : MonoBehaviour
{
    public static UnityEvent<string> eventShowNewHint = new UnityEvent<string>();

    [SerializeField] private GameObject content;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float timeBeforeAdditionalText = 15f;
    [SerializeField] private string additionalText = " (это окно можно закрыть)";
    
    private bool activated = true;
    private bool additionalTextAdded = false;

    private void Awake()
    {
        eventShowNewHint.AddListener(ShowNewHint);
    }

    private void Update()
    {
        if (!additionalTextAdded && activated)
        {
            timeBeforeAdditionalText -= Time.deltaTime;
            if (timeBeforeAdditionalText <= 0)
            {
                textMesh.text += additionalText;
                additionalTextAdded = true;
            }
        }
    }

    public void Activate(bool activate)
    {
        activated = activate;
        content.SetActive(activated);
    }

    private void ShowNewHint(string text)
    {
        textMesh.text = text;
        Activate(true);
        additionalTextAdded = false;
        timeBeforeAdditionalText = 15f;
    }
}
