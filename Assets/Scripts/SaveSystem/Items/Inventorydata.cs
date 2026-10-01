/*using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Inventorydata : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public string description;
    public Sprite itemsprite;

    public bool isFull;

    [Header("UI")]
    public Image icon;

    public void AddItem(string name, int qty, Sprite sprite, string desc)
    {
        itemName = name;
        quantity = qty;
        description = desc;
        itemsprite = sprite;
        isFull = true;

        if (icon != null && sprite != null)
        {
            icon.sprite = sprite;
            icon.enabled = true;
        }

    }

    public void RemoveItem()
    {
        itemName = "";
        quantity = 0;
        description = "";
        itemsprite = null;
        isFull = false;

        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}
*/