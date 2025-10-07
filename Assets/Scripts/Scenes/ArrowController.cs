using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private string targetScene; // fallback for inspector
    #region OnPressed
    public void OnPressed()
    {
        Debug.Log($"Arrow clicked: {gameObject.name}");
        // get scene name from object name
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Debug.Log($"Current scene: {currentScene}");
        string nextScene = null;

        // right = 90' clockwise (CW), left = 90' CCW 
        if (gameObject.name.Contains("Right"))
        {
            nextScene = GetNextScene(currentScene, "Clockwise");
        }
        else if (gameObject.name.Contains("Left"))
        {
            nextScene = GetNextScene(currentScene, "Counterclockwise");
        }
        else
        {
            nextScene = GetNextScene(currentScene);
        }

        Debug.Log($"Next scene calculated: {nextScene}");

        if (nextScene != null)
        {
            if (FadeController.Instance != null)
            {
                FadeController.Instance.FadeAndLoad(nextScene);
            }
            else
            {
                Debug.LogWarning("FadeController.Instance is null - using SceneController fallback");
                if (SceneController.scene_controller_instance == null)
                {
                    Debug.LogError("SceneController.scene_controller_instance is NULL! Make sure SceneController is in the scene.");
                    return;
                }
                SceneController.scene_controller_instance.Traverse_Scene(nextScene);
            }
        }
        else
        {
            Debug.LogWarning("Next scene is null - check scene naming convention");
        }
    }
    #endregion

    #region GetNextScene (Directed)
    private string GetNextScene(string currentScene, string direction)
    {
        // numbers are cardinal directions, doing so since it'll appear sequentially in project
        // 1 = North, 2 = E, 3 = S, 4 = west
        // moving right (clockwise): 1 > 2 > 3 > 4 > 1
        // moving left (CCW) 1 > 4 > 3 > 2 > 1
        int currentNumber = GetRoomNumber(currentScene);
        if (currentNumber == -1) return null;

        string roomName = GetRoomName(currentScene);
        if (string.IsNullOrEmpty(roomName)) return null;

        int nextNumber;
        if (direction == "Clockwise") // for wraparounds 1 > 4 CCW or 4 > 1 CW
        {
            nextNumber = (currentNumber == 4) ? 1 : currentNumber + 1;
        }
        else // moving CCW
        {
            nextNumber = (currentNumber == 1) ? 4 : currentNumber - 1;
        }

        // build target scene name
        string nextDirection = GetDirectionName(nextNumber);
        return $"{roomName} - {nextNumber} {nextDirection} Wall";
    }
    #endregion

    #region GetNextScene (Undirected)
    private string GetNextScene(string currentScene)
    {
        // If no direction specified, assume same room
        // Btw when we start working on other rooms in the house, can make another overload for "proceeding"(?)
        int currentNumber = GetRoomNumber(currentScene);
        Debug.Log(currentNumber);
        if (currentNumber == -1) return null;

        string roomName = GetRoomName(currentScene);
        if (string.IsNullOrEmpty(roomName)) return null;

        // build target scene name
        string nextDirection = GetDirectionName(currentNumber);
        return $"{roomName} - {currentNumber} {nextDirection} Wall";
    }
    #endregion

    #region GetRoomName
    private string GetRoomName(string sceneName)
    {
        // EX: extract "MC Room" from "MC Room - 1 North Wall"
        int dashIndex = sceneName.IndexOf(" - ");
        if (dashIndex > 0)
        {
            return sceneName.Substring(0, dashIndex);
        }
        Debug.LogWarning($"Couldn't extract room name from: {sceneName}. Please check naming convention");
        return null;
    }
    #endregion

    #region GetRoomNumber
    private int GetRoomNumber(string sceneName)
    {
        // extract number from scene name EX: "MC Room -1" will be 1
        if (sceneName.Contains(" 1 ")) return 1;
        if (sceneName.Contains(" 2 ")) return 2;
        if (sceneName.Contains(" 3 ")) return 3;
        if (sceneName.Contains(" 4 ")) return 4;

        Debug.LogWarning($"Couldn't find room number in scene name: {sceneName}");
        return -1;
    }
    #endregion

    private string GetDirectionName(int number)
    {
        switch (number)
        {
            case 1: return "North";
            case 2: return "East";
            case 3: return "South";
            case 4: return "West";
            default: return "";
        }
    }

    private void Start()
    {
        if (GetComponent<Collider2D>() == null && GetComponent<Collider>() == null)
        {
            Debug.LogWarning($"{gameObject.name} needs a collider!");
        }
    }
}