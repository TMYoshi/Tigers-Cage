#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class PlayerPlayerHelper : EditorWindow
{
    [MenuItem("Tiger Tools/Save Helper")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPlayerHelper>("Save Helper");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Save"))
        {
            Saves_System.SavePlayer();
            Debug.Log("Save Everything");
        }
        if (GUILayout.Button("Load"))
        {
            Saves_System.LoadPlayer();
            Debug.Log("Load Everything");
        }
    }
}
#endif
