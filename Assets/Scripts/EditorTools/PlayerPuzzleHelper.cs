#if UNITY_EDITOR
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class PlayerPuzzleHelper : EditorWindow
{

    string puzzleName = "";

    [MenuItem("Tiger Tools/Puzzle Helper")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPuzzleHelper>("Puzzle Helper");
    }

    private void OnGUI()
    {
        puzzleName = EditorGUILayout.TextField("Puzzle Name", puzzleName);

        if (GUILayout.Button("Complete Puzzle"))
        {
            InventoryManager.alreadyInteratedItems.Add(puzzleName);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
#endif
