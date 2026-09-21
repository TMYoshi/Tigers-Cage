using UnityEngine;

[RequireComponent(typeof(CutsceneManager))]
public class SetTransitionSceneToSomethingElseBaseOnSave : MonoBehaviour
{
    void Start()
    {
        CutsceneManager cutsceneManager = GetComponent<CutsceneManager>();

        if(SaveData.Instance.SceneIndex != "")
            cutsceneManager.nextSceneName = SaveData.Instance.SceneIndex;
    }
}
