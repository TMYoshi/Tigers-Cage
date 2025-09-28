using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController scene_controller_instance;
    public string finalSceneForCutscene;

    private void Awake()
    {
        if (scene_controller_instance == null)
        {
            scene_controller_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Traverse_Scene(string associated_scene)
    {
        SceneManager.LoadSceneAsync(associated_scene);
    }

    public void FadeAndLoadScene(string sceneName)
    {
        Debug.Log($"Starting fade transition to {sceneName}");
        StartCoroutine(FadeAndLoadRoutine(sceneName));
    }

    // New wrapper method with no parameters
    public void StartCutsceneFromMainButton()
    {
        Debug.Log("Button clicked, starting cutscene transition.");
        // Call the original method with your specified scene names
        FadeAndLoadSceneWithCutscene("Intro_Cutscene", finalSceneForCutscene);
    }

    public void FadeAndLoadSceneWithCutscene(string cutsceneSceneName, string finalSceneName)
    {
        Debug.Log($"Starting cutscene transition: {cutsceneSceneName} to {finalSceneName}");
        StartCoroutine(FadeAndLoadCutsceneRoutine(cutsceneSceneName, finalSceneName));
    }

    private IEnumerator FadeAndLoadRoutine(string sceneName)
    {
        if (FadeController.Instance == null)
        {
            Debug.LogError("FadeController not found. Loading scene without fade.");
            SceneManager.LoadScene(sceneName);
            yield break;
        }

        FadeController.Instance.FadeAndLoad(sceneName);
    }

    private IEnumerator FadeAndLoadCutsceneRoutine(string cutsceneSceneName, string finalSceneName)
    {
        if (FadeController.Instance == null)
        {
            Debug.LogError("FadeController not found. Loading scene without fade.");
            SceneManager.LoadScene(finalSceneName);
            yield break;
        }

        PlayerPrefs.SetString("NextSceneAfterCutscene", finalSceneName);
        FadeController.Instance.FadeAndLoad(cutsceneSceneName);
    }
}