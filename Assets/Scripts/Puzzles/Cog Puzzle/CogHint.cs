using System.Collections;
using UnityEngine;

public class CogHint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hintRenderer;
    [SerializeField] private AudioClip feedback_sfx_;
    private Coroutine fadeCoroutine;

    void Update()
    {
        if (PlayerInput.Instance.HintInput)
        {
            Debug.LogWarning("H hint detected");
            PlayerInput.Instance.HintInput = false; // reset instance 

            if(fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeHint(155f / 255f, 1.5f));
        }
    }

    IEnumerator FadeHint(float targetAlpha, float duration)
    {
        Color color = hintRenderer.color;
        float startAlpha = color.a;
        float elapsed = 0f;

        SFXManager.Instance.PlaySFXClip(feedback_sfx_);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsed/duration);
            hintRenderer.color = color;
            yield return null;
        }
    }
}
