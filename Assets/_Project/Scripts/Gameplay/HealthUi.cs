using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUi : MonoBehaviour
{
    [SerializeField] private GameObject prefabHearth;

    private MainCharacter mainCharacter;

    private List<GameObject> createdHearts = new List<GameObject>();

    private void Awake()
    {
        mainCharacter = FindAnyObjectByType<MainCharacter>();
    }

    private void Update()
    {
        if (mainCharacter == null)
        {
            return;
        }

        if (createdHearts.Count < mainCharacter.currentHealth)
        {
            for (int i = createdHearts.Count; i < mainCharacter.currentHealth; i++)
            {
                GameObject goHearth = Instantiate(prefabHearth, transform);
                createdHearts.Add(goHearth);
            }
        }
        else if (createdHearts.Count > mainCharacter.currentHealth)
        {
            for (int i = mainCharacter.currentHealth; i < createdHearts.Count; i++)
            {
                Destroy(createdHearts[i]);
                createdHearts.Remove(createdHearts[i]);
            }
        }
    }
}
