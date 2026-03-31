#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class PlayerInventoryHelper : EditorWindow
{

    string itemName = "";
    Texture2D itemImage = null;

    [MenuItem("Tiger Tools/Inventory Helper")]
    public static void ShowWindow()
    {
        GetWindow<PlayerInventoryHelper>("Inventory Helper");
    }

    private void OnGUI()
    {
        GUILayout.Label("Player Inventory Items", EditorStyles.boldLabel);
        itemImage = (Texture2D)EditorGUILayout.ObjectField("Item Image", itemImage, typeof(Texture2D), false);
        itemName = EditorGUILayout.TextField("Item Name", itemName);

        if (GUILayout.Button("Add Item"))
        {
            GameObject tempItem = new GameObject("New Inventory Item"); 
            InventoryItem itemToAdd = tempItem.AddComponent<InventoryItem>();
            itemToAdd.Init(itemName, Sprite.Create(itemImage, new Rect(0, 0, itemImage.width, itemImage.height), new Vector2(0.5f, 0.5f)));

            PlayerDialogItemState.MarkItemAsCollected(itemToAdd);
            PlayerDialogItemState.AddItemToInv(itemToAdd);

            Debug.Log("Item was added!");
        }
    }
}
#endif
