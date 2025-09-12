using UnityEngine;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public List<string> lines_;
    public Dialog assoc_dialog_box_;

    public bool WriteLines()
    {
        Debug.Log("Click!");
        assoc_dialog_box_.SetLines(lines_);
        return assoc_dialog_box_.PlayDialog();
    }
}
