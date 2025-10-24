using UnityEngine;

public class Populate_Item : MonoBehaviour
{
    public ItemSO item_so_;

    [Header("References")]
    [SerializeField] private InventoryItem inventory_item;
    [SerializeField] private Dialog dialog_;
    [SerializeField] private Sprite sprite_;

    private void Awake()
    {
        dialog_.convos_.Add(item_so_.dialogSO_);

        sprite_ = item_so_.sprite_;
        
    }

}
