using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{

    [Header("Assign Load Button to auto-diable")]
    [SerializeField] private Button loadButton;
    public void SaveGame()
    {   //Disable Load Button if no save file exists
        string path = Application.persistentDataPath + "/player.Journal";

        if (!File.Exists(path))
        {
            if(loadButton != null)
            {
                loadButton.interactable = false;
            }
        }
        Saves_System.SavePlayer();
    }

    public void LoadGame()
    {
        //Must always unpuase before chaning scenes
        Time.timeScale = 1f;
        Player_Data data = Saves_System.LoadPlayer();
        if (data == null) return;

        Saves_System.SetPendingLoad(data);
        SceneManager.LoadScene(data.sceneIndex);

       
    }
}