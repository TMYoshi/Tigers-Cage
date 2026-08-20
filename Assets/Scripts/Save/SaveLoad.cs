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

        UpdateLoadButtonState();

    }

    private void UpdateLoadButtonState(){

        if(loadButton != null)
        {
            loadButton.interactable = Saves_System.SaveFileExists();
        }
    }
    public void SaveGame()
    {   //Disable Load Button if no save file exists

        Debug.Log("Saving game click");
        Debug.Log("Game saved");
        Saves_System.SavePlayer();

        UpdateLoadButtonState();

        Debug.Log("Save completed");
    }

    public void LoadGame()
    {
        Debug.Log("Load button click");
        //Must always unpuase before chaning scenes
        Time.timeScale = 1f;
        Player_Data data = Saves_System.LoadPlayer();

        if (data == null)
        {
            Debug.LogError("Failed to load game data");
            return;
        }

        Debug.Log("Data loaded");
        Saves_System.SetPendingLoad(data);
        SceneManager.LoadScene(data.sceneIndex);

        Debug.LogWarning("Buttons not working, caused from main menue scence");
       
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

// Debug.LogWarning("Buttons not working, caused from main menue scence");