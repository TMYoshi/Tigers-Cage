using System.Collections;
using UnityEngine;

public class StartCutsceneOnMouseDown : MonoBehaviour
{
    public string cutsceneSceneName = "Ending_Cutscene";
    public string finalSceneName = "GameOver";

    private void OnMouseDown()
    {
        Debug.Log("Sprite clicked. Waiting for a moment before starting cutscene.");
        StartCoroutine(StartCutsceneAfterDelay());
    }

    private IEnumerator StartCutsceneAfterDelay()
    {
        // wait for dialog to finish
        yield return new WaitForSeconds(.7f);

        Debug.Log("Starting cutscene transition.");
        if (SceneController.scene_controller_instance != null)
        {
            SceneController.scene_controller_instance.FadeAndLoadSceneWithCutscene(cutsceneSceneName, finalSceneName); // fade to black should persist with menus/cutscene transitions 
        }
        else
        {
            Debug.LogError("SceneController instance not found!");
        }
    }
}