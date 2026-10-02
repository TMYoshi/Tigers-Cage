using UnityEngine;
using System.Collections.Generic;

public class MasterList : MonoBehaviour
{
    //scriptable objects are currently only used to store/restore save files
    //each save file will look up to see if the items are in the collected SO
    public static MasterList Instance;
    public List<ItemSO> EveryCollectableSO;

    void Awake()
    {
        Instance = this;
    }
}
