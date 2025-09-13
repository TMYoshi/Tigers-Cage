using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingData : MonoBehaviour
{
    public List<int> ID;

    void Update()
    {
        SaveSystem.Save(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.Load();

        ID = data.ID;
    }
}
