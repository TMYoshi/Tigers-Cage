using UnityEngine;
using UnityEditor;

public class PlayerInventoryHelper : EditorWindow
{
    [MenuItem("Tiger Tools/Inventory Helper")]
    public static void ShowWindow()
    {
        GetWindow<PlayerInventoryHelper>("Inventory Helper");
    }

    private void OnGUI()
    {
        GUILayout.Label("Player Inventory Items", EditorStyles.boldLabel);
        Texture2D itemImage = null;
        itemImage = (Texture2D)EditorGUILayout.ObjectField("Item Image", itemImage, typeof(Texture2D), false);

        string itemName = "";
        itemName = EditorGUILayout.TextField("Item Name", itemName);

        if (GUILayout.Button("Add Item"))
        {
            Debug.Log("Item was added!");
        }
    }
}
