using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WindowWithButtons : MonoBehaviour
{
    public static UnityEvent eventShowNextData = new UnityEvent();

    [Header("Changeable values")]
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Links to objects")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image mainImage;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private List<Button> myButtons = new List<Button>();
    [SerializeField] private List<TextMeshProUGUI> textOfMyButtons = new List<TextMeshProUGUI>();

    [Header("Links to assets")]
    [SerializeField] private List<DataForWindowWithButtons> listWithData = new List<DataForWindowWithButtons>();

    private int indexInList = 0;

    private void Awake()
    {
        eventShowNextData.AddListener(ShowNextData);

        foreach (Button button in myButtons)
        {
            button.onClick.AddListener(() => Activate(false));
        }
    }

    private void ShowNextData()
    {
        Debug.Log($"WindowWithButtons: ShowNextData: begin");

        DataForWindowWithButtons data = listWithData[indexInList];

        if (data.end)
        {
            TextOutro.eventActivate.Invoke();
            return;
        }

        // спрайт
        if (data.showSprite)
        {
            mainImage.gameObject.SetActive(true);
            mainImage.sprite = data.sprite; 
        }
        else
        {
            mainImage.gameObject.SetActive(false);
        }

        // главный текст
        mainText.text = data.mainText;
        
        // Включаем кнопки и задаем текст
        for (int i = 0; i < data.textForButtons.Count; i++)
        {
            myButtons[i].gameObject.SetActive(true);
            textOfMyButtons[i].text = data.textForButtons[i];

            if (data.goToNextScene)
            {
                myButtons[i].onClick.AddListener(LoadNextScene);
            }
        }

        // Выключаем остальные кнопки 
        for (int i = data.textForButtons.Count; i < myButtons.Count; i++)
        {
            myButtons[i].gameObject.SetActive(false);
        }

        Activate(true);
        
        indexInList++;
    }

    public void Activate(bool activate)
    {
        if (activate)
        {
            Time.timeScale = 0f;
            canvasGroup.gameObject.SetActive(true);
            StartCoroutine(FadeCanvasGroup(0f, 1f, fadeDuration));
        }
        else
        {
            StartCoroutine(FadeCanvasGroup(1f, 0f, fadeDuration));
            canvasGroup.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float targetAlpha, float duration)
    {
        float startTime = Time.realtimeSinceStartup;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime = Time.realtimeSinceStartup - startTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha; // Убедимся, что альфа принимает точное значение в конце.
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        foreach (Button button in myButtons)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => Activate(false));
        }
    }
}
