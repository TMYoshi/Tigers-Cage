using UnityEngine;

public class Item : MonoBehaviour
{
    public string[] lines_;
    public Dialog assoc_dialog_box_;
    void OnMouseDown()
    {
        Debug.Log("Click!");
        assoc_dialog_box_.SetLines(lines_);
        assoc_dialog_box_.Update();
    }
}
