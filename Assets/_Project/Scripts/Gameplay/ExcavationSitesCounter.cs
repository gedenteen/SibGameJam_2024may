using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavationSitesCounter : MonoBehaviour
{
    public static ExcavationSitesCounter Instance {get; private set;} = null;
 
    public int countOfNotExcavatedSites {get; private set;} = 0; // количество НЕ раскопанных ям
    public List<ExcavationSite> excavationSites = new List<ExcavationSite>();

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
        excavationSites.Add(excavationSite);
        Debug.Log($"countOfNotExcavatedSites={countOfNotExcavatedSites}");
    }

    public void Remove(ExcavationSite excavationSite)
    {
        countOfNotExcavatedSites--;
        excavationSites.Remove(excavationSite);
        Debug.Log($"countOfNotExcavatedSites={countOfNotExcavatedSites}");
    }

    public void TeleportToExcavationSite(Transform objectToMove)
    {
        if (excavationSites.Count > 0)
        {
            objectToMove.position = excavationSites[0].transform.position;
        }
        else
        {
            Debug.Log($"There is no excavation sites for dig!");
        }
    }
}
