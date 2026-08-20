using UnityEngine;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;//change
using UnityEngine.SceneManagement;
using System.Collections.Generic;//change
using System.Data.Common;//change

public static class Saves_System
{
    //Both Private prevents scene loading destroy objects
    private static Player_Data pendingLoadData; 
    private static bool hookedSceneLoaded = false;

    public static string SavePath
    {
        get
        {
            return Application.persistentDataPath + "/Player.Journal.json";
        }
    }

    public static bool SaveFileExists()
    {
        return File.Exists(SavePath);
    }
    public static void SavePlayer()
    {
        string path = SavePath;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //get current scene index to save and load back into the same scene

        //Find inventory in scene (if exists)
        InventoryManager inv = Object.FindAnyObjectByType<InventoryManager>();

        var inventorySlots = (inv != null) ? inv.BuildInventorySaveData() : null;
        var collectedIds = (inv != null) ? inv.BuildCollectedItemsSaveData() : null;

        //Create Save file object
        Player_Data data = new Player_Data(
            currentSceneIndex, 
            inventorySlots, 
            collectedIds);

       string json = JsonUtility.ToJson(data, true);

       File.WriteAllText(path, json);
        Debug.Log("Game Saved yeyeyey!!!");
        Debug.Log("Saved location: " + path);
    }

    public static Player_Data LoadPlayer()
    {
        string path = SavePath;

        if (!File.Exists(path))
        {
            Debug.LogError("Save File Not found in" + path);
            return null;
        }
          
          string json = File.ReadAllText(path); // 

          Player_Data data = JsonUtility.FromJson<Player_Data>(json); //takes json and converts it into a player data object


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
            inv.ApplyCollectedItemsSaveData(pendingLoadData.collectedItemIds);
            inv.ApplyInventorySaveData(pendingLoadData.inventorySlots);
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
