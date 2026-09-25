#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerSaveHelper : EditorWindow
{
    [MenuItem("Tiger Tools/Save Helper")]
    public static void ShowWindow()
    {
        GetWindow<PlayerSaveHelper>("Save Helper");
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Wipe Saved Scene"))
        {
            SaveData.Instance.SceneIndex = "";
            SaveData.Instance.SavePlayer();
        }

        if(GUILayout.Button("Wipe save"))
        {
            SaveData.Instance.CutsceneSaved = new bool[3];
            SaveData.Instance.SceneIndex = "";
            SaveData.Instance.InventorySlots = new List<InventorySlotData>();
            SaveData.Instance.CollectedItemIds = new List<string>();
            SaveData.Instance.UnlockedJournalEntries = new List<JournalPageSaveData>();
        }
    }
}
#endif
