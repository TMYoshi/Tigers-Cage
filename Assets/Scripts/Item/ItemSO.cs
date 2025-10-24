using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Template", menuName = "Item/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Inventory Item")]
    public string item_name_;
    public int quantity_;
    public AudioClip audio_clip_;
    public string desc_;
    public SpecialItems special_events_;
    public bool collectable_;

    [Header("Other Components")]
    public DialogSO dialogSO_;
    public Collider2D collider_;
    public Sprite sprite_;
}
