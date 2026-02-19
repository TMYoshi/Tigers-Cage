using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;//change
using UnityEngine.SceneManagement;
using System.Collections.Generic;//change
using System.Data.Common;//change

public static class Saves_System
{
    //Both Private prevents scene loading destroy objects
    private static Player_Data pendingLoadData; 
    private static bool hookedSceneLoaded = false;
    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.Journal";
        FileStream stream = new FileStream(path, FileMode.Create);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //Find inventory in scene (if exists)
        InventoryManager inv = Object.FindAnyObjectByType<InventoryManager>();

        var inventorySlots = (inv != null) ? inv.BuildInventorySaveData() : null;
        var collectedIds = (inv != null) ? inv.BuildCollectedItemsSaveData() : null;

        //Create Save file object
        Player_Data data = new Player_Data(currentSceneIndex, inventorySlots, collectedIds);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Game Saved yeyeyey!!!");
    }

    public static Player_Data LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.Journal";

        if (!File.Exists(path))
        {
            Debug.LogError("Save File Not found in" + path);
            return null;
        }
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Player_Data data = formatter.Deserialize(stream) as Player_Data;
            stream.Close();

            Debug.Log("game Loaded");
            return data;
        }

    public static void SetPendingLoad(Player_Data data)
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
            inv.ApplyInventorySaveData(pendingLoadData.inventorySlots);
            inv.ApplyCollectedItemsSaveData(pendingLoadData.collectedItemIds);//prevent items to duplicate.load once

            Debug.Log("Inventory applied after scene load");
        }
        else
        {
            Debug.Log("Not found");
        }

        pendingLoadData = null;
    }
}
