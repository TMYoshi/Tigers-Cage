using UnityEngine;
using UnityEditor;

public class PlayerInventoryHelper : EditorWindow
{

    [MenuItem("Tiger Tools/Inventory Helper")]
    public static void ShowWindow()
    {
        GetWindow<PlayerInventoryHelper>("Inventory Helper");
    }
}
