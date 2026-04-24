using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad Instance;
    [Header("Assign Load Button to auto-diable")]
    [SerializeField] private Button loadButton;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        string path = Application.persistentDataPath + "/Player.Journal";

        if(loadButton != null)
        {
            loadButton.interactable = File.Exists(path);
        }
    }
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

    public static void QuickSave()
    {
        if(Instance != null)
        {
            Instance.SaveGame();
        }
        else
        {
            Debug.LogWarning("No saveLoad Instance found in scene");
        }
    }

    public static void QuickLoad()
    {
        if(Instance != null)
        {
            Instance.LoadGame();
        }
        else
        {
            Debug.LogWarning("No saveload instacne found");
        }
    }
}