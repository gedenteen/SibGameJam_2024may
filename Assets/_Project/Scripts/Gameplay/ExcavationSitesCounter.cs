using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavationSitesCounter : MonoBehaviour
{
    public static ExcavationSitesCounter Instance {get; private set;} = null;
 
    public int countOfNotExcavatedSites {get; private set;} = 0; // количество НЕ раскопанных ям

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Add(ExcavationSite excavationSite)
    {
        countOfNotExcavatedSites++;
        Debug.Log($"countOfNotExcavatedSites={countOfNotExcavatedSites}");
    }

    public void Remove(ExcavationSite excavationSite)
    {
        countOfNotExcavatedSites--;
        Debug.Log($"countOfNotExcavatedSites={countOfNotExcavatedSites}");
    }
}
