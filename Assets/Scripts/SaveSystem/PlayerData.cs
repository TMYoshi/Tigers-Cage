using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<int> ID;

    public PlayerData (SettingData Setting)
    {
        ID = Setting.ID;
    }
}
