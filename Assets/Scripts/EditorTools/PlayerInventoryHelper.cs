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
        Sprite itemImage = null;
        itemImage = (Sprite)EditorGUILayout.ObjectField("Item Image", itemImage, typeof(Sprite), false);

        string itemName = "";
        itemName = EditorGUILayout.TextField("Item Name", itemName);

        if (GUILayout.Button("Add Item"))
        {
            GameObject tempItem = new GameObject("New Inventory Item"); 
            InventoryItem itemToAdd = tempItem.AddComponent<InventoryItem>();
            
            itemToAdd.Init(itemName, itemImage);

            PlayerDialogItemState.MarkItemAsCollected(itemToAdd);
            PlayerDialogItemState.AddItemToInv(itemToAdd);

            Debug.Log("Item was added!");
        }
    }
}
