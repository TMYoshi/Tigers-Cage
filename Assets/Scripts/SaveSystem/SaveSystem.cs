using UnityEngine;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;//change
using UnityEngine.SceneManagement;
using System.Collections.Generic;//change
using System.Data.Common;//change

public static class SaveSystem
{
    //Both Private prevents scene loading destroy objects
    private static PlayerData pendingLoadData; 
    private static bool hookedSceneLoaded = false;

    public static string SavePath
    {
        get
        {
            return Application.persistentDataPath + "/player.json";
        }
    }

    public static void Save(SaveData _save)
    {
        string path = Path.Combine(Application.persistentDataPath, "player.json");
        PlayerData data = new PlayerData(_save);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public static PlayerData Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.json");
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in: " + path);
            return null;
        }
    }

    public static void SetPendingLoad(PlayerData data)
    {
        pendingLoadData = data;

        if (!hookedSceneLoaded)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            hookedSceneLoaded = true;
        }
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(pendingLoadData == null) return;

        InventoryManager inv = Object.FindAnyObjectByType<InventoryManager>();

        if(inv != null)
        {
            inv.ApplyCollectedItemsSaveData(pendingLoadData.CollectedItemIds);
            inv.ApplyInventorySaveData(pendingLoadData.InventorySlots);
            //prevent items to duplicate.load once

            Debug.Log("Inventory applied after scene load");
        }
        else
        {
            Debug.Log("Not found");
        }

        pendingLoadData = null;
    }
}
