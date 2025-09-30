using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance { get; private set; }

    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float sceneLoadDelay = 0.01f;

    public event Action onFadeInComplete;

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

    private IEnumerator TransitionRoutine(string sceneName)
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.Play("BlinkingAnimationStart");
            yield return new WaitForSeconds(GetAnimationLength("BlinkingAnimationStart")); // currently fade to black, actual animation WIP
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(sceneLoadDelay);

        onFadeInComplete?.Invoke();

        if (transitionAnimator != null)
        {
            transitionAnimator.Play("BlinkingAnimationEnd"); // currently fade out of black
            yield return new WaitForSeconds(GetAnimationLength("BlinkingAnimationEnd"));
        }

    }

    private float GetAnimationLength(string animationName)
    {
        if (transitionAnimator == null) return 0f;

        AnimationClip[] clips = transitionAnimator.runtimeAnimatorController.animationClips;

        foreach(AnimationClip clip in clips)
        {
            if(clip.name == animationName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning($"Animation '{animationName}' not found. Using default delay.");
        return 0.5f;
    }
}