using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Equip_Button : MonoBehaviour
{
    public static Equip_Button Instance;
    [SerializeField]
    private string equipped_item_ = "None";
    // private InventoryItem equipped_item_; ok lowkey not sure if this will be problemativ later since the inventory only stores the item's info instead of the item itself butttt やってみようか

    [SerializeField]
    // private InventoryItem selected_item_;
    private string selected_item_ = "None";
    public TMP_Text equip_text_;

    // Another Singleton oh-em-gee
    // Lowkey prob/should definetly alter this cause like realistically is the equip button always gonna be there lol
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Onclick()
    {
        if (selected_item_ != "" && selected_item_ != equipped_item_)
        {
            Debug.Log("Equipping " + selected_item_);
            equipped_item_ = selected_item_;
            equip_text_.text = "Equipped";
        }
        // Unequips if already equipped or if selecting an empty slot
        else
        {
            // TODO: change text to say "Unequip?" on hover.
            Debug.Log("Unequipping " + selected_item_);
            equipped_item_ = "";
            equip_text_.text = "Equip";
        }
    }

    public void SetSelected(string selected_item)
    {
        Debug.Log(selected_item + " selected");
        selected_item_ = selected_item;
        if (selected_item_ != "" || selected_item_ != equipped_item_)
        {
            equip_text_.text = "Equip";
        }
        else
        {
            equip_text_.text = "Equipped"; // TODO: lowkey can't keep looking at the text lol, equipment should work tho
        }
    }

    public string GetEquipped()
    {
        return equipped_item_;
    }
}
