using UnityEngine;



//Each document item is asset which are stored with text,title,image, and unlock status
//Pure data like a file of an item
[CreateAssetMenu(fileName = "DocumentItem", menuName = "Scriptable Objects/DocumentItem")]
public class DocumentItem : ScriptableObject
{
    public string documentTitle;
    [TextArea] public string documentText;
    public Sprite documentImage;
    public bool isUnlocked = false;
    
}
