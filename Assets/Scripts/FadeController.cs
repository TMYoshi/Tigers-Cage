using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private Canvas fadeCanvas;

    // A static flag to track if we should fade in when scene starts
    private static bool shouldFadeInOnLoad = false;

    // Define a delegate and event for when the fade-in is complete
    public delegate void FadeInCompleteAction();
    public event FadeInCompleteAction onFadeInComplete;

    private void Awake()
    {
        Instance = this;

        if (fadeCanvas == null)
        {
            fadeCanvas = GetComponent<Canvas>();
            if (fadeCanvas == null)
            {
                fadeCanvas = GetComponentInParent<Canvas>();
            }
        }

        if (fadeCanvas != null)
        {
            fadeCanvas.sortingOrder = 999;
            fadeCanvas.overrideSorting = true;
        }

        Debug.Log($"FadeController initialized for scene: {SceneManager.GetActiveScene().name}");
    }

    private void Start()
    {
        if (shouldFadeInOnLoad)
        {
            Debug.Log("Scene loaded - starting fade in from black");
            StartCoroutine(HandleSceneStartFadeIn());
        }
        else
        {
            // Ensure we start with fade image hidden
            if (fadeImage != null)
            {
                Color color = fadeImage.color;
                color.a = 0f;
                fadeImage.color = color;
                fadeImage.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator HandleSceneStartFadeIn()
    {
        if (fadeImage == null)
        {
            Debug.LogError("FadeImage is null!");
            shouldFadeInOnLoad = false;
            yield break;
        }

        // Start with black screen
        fadeImage.gameObject.SetActive(true);
        if (fadeCanvas != null)
        {
            fadeCanvas.gameObject.SetActive(true);
        }

        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        // Wait a frame to ensure everything is rendered
        yield return null;
        yield return new WaitForSeconds(0.1f);

        // Fade from black to transparent
        Debug.Log("Fading from black to clear");
        yield return StartCoroutine(FadeToAlpha(0f));

        shouldFadeInOnLoad = false;
        Debug.Log("Fade in complete");

        // Notify any listeners that the fade-in is complete
        onFadeInComplete?.Invoke();
    }

    public void FadeAndLoad(string sceneName)
    {
        Debug.Log($"Starting fade transition to: {sceneName}");
        StartCoroutine(FadeAndLoadSceneRoutine(sceneName));
    }

    private IEnumerator FadeAndLoadSceneRoutine(string sceneName)
    {
        // Set flag so the next scene knows to fade in
        shouldFadeInOnLoad = true;

        if (fadeImage == null)
        {
            Debug.LogError("FadeImage is null!");
            yield break;
        }

        // Ensure fade elements are active
        fadeImage.gameObject.SetActive(true);
        if (fadeCanvas != null)
        {
            fadeCanvas.gameObject.SetActive(true);
        }

        // Fade to black
        Debug.Log("Fading to black");
        yield return StartCoroutine(FadeToAlpha(1f));

        // Load the new scene
        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator FadeToAlpha(float targetAlpha)
    {
        if (fadeImage == null)
        {
            Debug.LogError("FadeImage is null in FadeToAlpha!");
            yield break;
        }

        float startAlpha = fadeImage.color.a;
        Debug.Log($"Fading from {startAlpha} to {targetAlpha}");

        if (Mathf.Approximately(startAlpha, targetAlpha))
        {
            Debug.Log("Already at target alpha");
            yield break;
        }

        fadeImage.gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / fadeDuration;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            fadeImage.color = color;

            yield return null;
        }

        // Ensure exact final value
        Color finalColor = fadeImage.color;
        finalColor.a = targetAlpha;
        fadeImage.color = finalColor;

        Debug.Log($"Fade complete - final alpha: {finalColor.a}");

        // Hide if fully transparent
        if (targetAlpha <= 0f)
        {
            fadeImage.gameObject.SetActive(false);
        }
    }

    // Validation in editor
    private void OnValidate()
    {
        if (fadeImage == null)
        {
            Debug.LogWarning($"{gameObject.name}: FadeImage not assigned!");
        }
    }
}