using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneTransitionHelper : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private bool useTigerAnimation = false;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ExecuteTransition);
    }

    private void ExecuteTransition()
    {
        if (SceneController.scene_controller_instance == null) {
            Debug.LogError("SceneController not found in memory.");
                return;
        }

        if (useTigerAnimation)
        {
            SceneController.scene_controller_instance.MainMenuTraverse(targetScene);
        }
        else
        {
            SceneController.scene_controller_instance.FadeAndLoadScene(targetScene);
        }
    }
}
