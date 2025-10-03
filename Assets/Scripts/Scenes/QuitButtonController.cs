using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuitButtonController : MonoBehaviour
{
    public void QuitApplication()
    {
        StartCoroutine(FadeAndQuit()); // fade also stays here for quitout
    }

    private IEnumerator FadeAndQuit()
    {
        if (FadeController.Instance == null)
        {
            Debug.LogWarning("Fade controller not found.");
            Application.Quit();
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #endif
            yield break;
        }

        yield return StartCoroutine(FadeController.Instance.FadeToAlpha(1f));
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }
}