using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance { get; private set; }

    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private CanvasGroup fadeCanvasGroup;

    public event Action onFadeInComplete;
    bool alreadyLoading = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void FadeAndLoad(string sceneName)
    {
        if(alreadyLoading) return;
        StartCoroutine(TransitionRoutine(sceneName));
    }

    public void FadeAndLoadWithCutscene(string cutsceneScene, string finalScene)
    {
        PlayerPrefs.SetString("NextSceneAfterCutscene", finalScene);
        PlayerPrefs.Save();
        FadeAndLoad(cutsceneScene);
    }

    public IEnumerator FadeToAlpha(float targetAlpha, float duration = 1f)
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogWarning("FadeCanvasGroup not assigned. Cannot fade to alpha.");
            yield break;
        }

        float startAlpha = fadeCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration); //
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }

    bool finishedBlinkAnimation;
    public void setFinishedBlinkAnimation() => finishedBlinkAnimation = true;

    private IEnumerator TransitionRoutine(string sceneName)
    {
        alreadyLoading = true;
        finishedBlinkAnimation = false;
        transitionAnimator.SetBool("Blink", true);

        yield return new WaitUntil(() => finishedBlinkAnimation);
        AsyncOperation scene = SceneController.scene_controller_instance.TraverseScene(sceneName);
        yield return new WaitUntil(() => scene.isDone);

        transitionAnimator.SetBool("Blink", false);
        alreadyLoading = false;
        onFadeInComplete?.Invoke();
    }
}
