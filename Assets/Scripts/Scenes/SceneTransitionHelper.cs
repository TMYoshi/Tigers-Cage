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
        if (SceneController.Instance == null) {
            Debug.LogError("SceneController not found in memory.");
                return;
        }

        if (useTigerAnimation)
        {
            SceneController.Instance.MainMenuTraverse(targetScene);
        }
        else
        {
            SceneController.Instance.FadeAndLoadScene(targetScene);
        }
    }
}
