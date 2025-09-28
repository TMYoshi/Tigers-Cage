using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public List<Interaction> _Interactions;
}
[System.Serializable]
public class Interaction
{
    //key is the name of the item
    public string key;
    [SerializeReference]
    public SpecialItems effect;
}
