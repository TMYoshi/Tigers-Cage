using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Allows for scene controller to be accessed anywhere as all scene manager objects share this variable
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/static
    public static SceneController scene_controller_instance;

    // Awake excutes before Start for use in references, inheritted by MonoBehavior
    private void Awake()
    {
        if (scene_controller_instance == null)
        {
            // Prevents destruction of object upon loading new scene
            scene_controller_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Ref: https://www.youtube.com/watch?v=E25JWfeCFPA
    public void Traverse_Scene(string associated_scene)
    {
        SceneManager.LoadSceneAsync(associated_scene);
    }
}
