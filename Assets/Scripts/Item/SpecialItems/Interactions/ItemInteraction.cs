using UnityEngine;
public abstract class Interaction : MonoBehaviour
{
    //key is the name of the item
    //Effects has to be defined
    public string key;
    public abstract void ExecuteEffect(PlayerStateManager _context);
}

