using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController scene_controller_instance;

    [SerializeField] private string cutsceneScene = "Intro_Cutscene";
    [SerializeField] private string sceneAfterCutscene;

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

    // scene load without fade (fallback)
    public void Traverse_Scene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    // call from main menu button, starts game with cutscene
    // should be ok to stay once we've introduced save files/load game
    public void StartGameWithCutscene()
    {
        if (FadeController.Instance != null)
        {
            FadeController.Instance.FadeAndLoadWithCutscene(cutsceneScene, sceneAfterCutscene);
        }
        else
        {
            Debug.LogWarning("FadeController not found, loading directly");
            SceneManager.LoadScene(cutsceneScene);
        }
    }


    public void FadeAndLoadScene(string sceneName)
    {
        if (FadeController.Instance != null)
        {
            FadeController.Instance.FadeAndLoad(sceneName);
        }
        else
        {
            Debug.LogWarning("FadeController not found, loading directly instead.");
            SceneManager.LoadScene(sceneName);
        }
    }

    public void FadeAndLoadSceneWithCutscene(string cutsceneSceneName, string finalSceneName)
    {
        if(FadeController.Instance != null)
        {
            FadeController.Instance.FadeAndLoadWithCutscene(cutsceneSceneName, finalSceneName);
        }
        else
        {
            Debug.LogWarning("Fade controller not found, loading directly instead.");
            SceneManager.LoadScene(cutsceneSceneName);
        }
    }
}