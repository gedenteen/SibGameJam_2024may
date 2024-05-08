using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelGameOver : MonoBehaviour
{
    [SerializeField] private GameObject myContent;

    private void Awake()
    {
        GameplayEvents.eventGameOver.AddListener(OnGameOver);
    }

    private void Activate(bool activate)
    {
        myContent.SetActive(activate);
    }

    private void OnGameOver()
    {
        Activate(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
