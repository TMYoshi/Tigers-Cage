using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Equip_Button : MonoBehaviour
{
    [SerializeField]
    private string equipped_item_ = "None";

    [SerializeField]
    private string selected_item_ = "None";
    public TMP_Text equip_text_;

    public void Onclick()
    {
        if (selected_item_ != "" && selected_item_ != equipped_item_)
        {
            Debug.Log("Equipping " + selected_item_);
            equipped_item_ = selected_item_;
            equip_text_.text = "Equipped";
        }
    }

    public void SetSelected(string selected_item)
    {
        Debug.Log(selected_item + " selected");
        selected_item_ = selected_item;
        if (selected_item_ != equipped_item_)
        {
            equip_text_.text = "Equip";
        }
        else
        {
            equip_text_.text = "Equipped";
        }
    }
}
