using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{

    public void SaveGame()
    {
        Saves_System.SavePlayer();
    }

    public void LoadGame()
    {
        Player_Data data = Saves_System.LoadPlayer();
        if (data == null) return;

        Saves_System.SetPendingLoad(data);
        SceneManager.LoadScene(data.sceneIndex);
    }
}