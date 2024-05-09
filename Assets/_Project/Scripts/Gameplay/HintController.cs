using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintController : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float timeBeforeAdditionalText = 15f;
    [SerializeField] private string additionalText = " (это окно можно закрыть)";
    
    private bool activated = true;
    private bool additionalTextAdded = false;

    public void Activate(bool activate)
    {
        activated = activate;
        content.SetActive(activated);
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
}
