using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Template", menuName = "Item/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Inventory Item")]
    public string item_name_;
    public string desc_;
    public Sprite inv_sprite_;
}
