using UnityEngine;
[CreateAssetMenu(menuName = "Crafting/New Item")]
public class ItemComCreation : ItemCombinations 
{
    public string ResultName;
    public Sprite ResultImage;
    [TextArea]
    public string ResultDescription;
}
