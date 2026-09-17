using UnityEngine;
using System.Collections.Generic;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;

    public string SceneIndex = "";
    public List<InventorySlotData> InventorySlots = new List<InventorySlotData>();
    public List<string> CollectedItemIds = new List<string>();
    public List<JournalPageSaveData> UnlockedJournalEntries = new List<JournalPageSaveData>();

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayer();
    }

    public void SavePlayer()
    {
        SaveSystem.Save(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.Load();

        SceneIndex = data.SceneIndex;
        InventorySlots = data.InventorySlots;
        CollectedItemIds = data.CollectedItemIds;
        UnlockedJournalEntries = data.UnlockedJournalEntries;
    }
}
