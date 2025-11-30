using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;

public static class Saves_System
{
    public static void SavePlayer(Player_Data data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.Journal";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Game Saved yeyeyey!!!");
    }

    public static Player_Data LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.Journal";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Player_Data data = formatter.Deserialize(stream) as Player_Data;
            stream.Close();

            Debug.Log("game Loaded");
            return data;
        }
        else
        {
            Debug.LogError("Save File Not found in" + path);
            return null;
        }
        
    }
    


}
