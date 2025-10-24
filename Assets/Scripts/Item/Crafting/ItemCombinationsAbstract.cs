using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public abstract class ItemCombinations : ScriptableObject
{
    public string _item1;
    public string _item2;
    public bool CanCraft(string item1, string item2)
    {
        if ((_item1 == item1 && _item2 == item2) ||
            (_item2 == item1 && _item1 == item2)) return true;

        return false;
    }
}