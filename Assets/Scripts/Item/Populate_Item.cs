using UnityEditor.EditorTools;
using UnityEngine;

public class Populate_Item : MonoBehaviour
{
    public ItemSO item_so_;

    [Header("References")]
    [Tooltip("These should all be from components!!\n Note that you have to make your own box collider though!!")]
    [SerializeField] private InventoryItem inventory_item;
    [SerializeField] private Dialog dialog_;
    [SerializeField] private SpriteRenderer sprite_;

    private void Awake()
    {
        dialog_.convos_.Add(item_so_.dialogSO_);

        sprite_.sprite = item_so_.sprite_; // sprite_ is a sprite render not a sprite!

        inventory_item.SetItemName(item_so_.item_name_);
        inventory_item.SetQuantity(item_so_.quantity_);
        inventory_item.SetAudioClip(item_so_.audio_clip_);
        inventory_item.SetDesc(item_so_.desc_);
        inventory_item.SetSprite(item_so_.sprite_);
        inventory_item.AssignSpecialEvents(item_so_.special_events_);
        inventory_item.SetCollectable(item_so_.collectable_);
    
    }

}
