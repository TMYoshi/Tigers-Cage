using System.Linq.Expressions;
using UnityEngine;

[System.Serializable]
public abstract class ItemCombinations : ScriptableObject
{
    public string _item1;
    public string _item2;
    public abstract void ExecItemCombo();
    public void CraftCombo(string item1, string item2)
    {
        try
        {
            if ((_item1 == item1 && _item2 == item2) ||
                (_item2 == item1 && _item1 == item2)) ExecItemCombo();
        }
        catch
        {
            Debug.LogWarning($"Failed to craft combo");
        }
    }
}