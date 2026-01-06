using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Save/Load")]
    public SaveLoad saveLoadManager;

    [Header("UI Refrences")]
    public GameObject JournalUI;// pause menu panel first
    public GameObject PauseBackground;// Journal Panel with three buttons
    public GameObject tableofContentes; //Second UI 
    public TMP_Text titleText;
    public TMP_Text contentText;
    public Image documentImage;

    public GameObject documentPage;

    [Header("TOC Buttons")]
    public Button[] documentButtons;

    private bool isPaused = false;

    void Start()
    {
        PauseBackground.SetActive(false);
        JournalUI.SetActive(false);

        UpdateTOCButtons();

    }
    //Check if the key 'J" is pressed. it will pause the game
    void Update()
    {
        if (Input.GetButtonDown("Journal"))
        {
            Debug.Log("J is pressed ");
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    //Game will freeze and show UI pause menue
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        JournalUI.SetActive(true);// shows pause menue
        documentPage.SetActive(false);
        tableofContentes.SetActive(false);
        PauseBackground.SetActive(true);

        Debug.Log("games is paused ");
    }

    public void ResumeGame()
    {
        Debug.Log("games is unpaused ");
        isPaused = false;
        Time.timeScale = 1f;
        JournalUI.SetActive(false);
        documentPage.SetActive(false);
    }

    public void OpenJournal()
    {
        PauseBackground.SetActive(false);

        tableofContentes.SetActive(true);
        Debug.Log("Opened table of contetnents");

    }

    void UpdateTOCButtons()
    {
        var data = JournalDataManager.Instance.allDocuments;

        for (int i = 0; i < documentButtons.Length; i++)
        {
            bool unlocked = data[i].isUnlocked;
            documentButtons[i].gameObject.SetActive(unlocked);

            int index = i; // local copy for lambda
            documentButtons[i].onClick.RemoveAllListeners();
            documentButtons[i].onClick.AddListener(() => OpenDocument(index));
        }
    }

    public void OpenDocument(int index)
    {
        var doc = JournalDataManager.Instance.allDocuments[index];
        titleText.text = doc.documentTitle;
        contentText.text = doc.documentText;
        documentImage.sprite = doc.documentImage;
        tableofContentes.SetActive(false);
        documentPage.SetActive(true);

        Debug.Log($"Opened document: {doc.documentTitle}");
    }

    public void QuitToMainMenu()
    {
        // Make sure time is running again
        Time.timeScale = 1f;


        // Load main menu directly
        SceneManager.LoadScene("Main Menu");
    }


    public void SaveGame()
    {
        if (saveLoadManager != null)
        {
            saveLoadManager.SaveGame();
            Debug.Log("Game saved via pause menue!");
        }
    }

    public void LoadGame()
    {
        saveLoadManager.LoadGame();
        Debug.Log("Game load");
    }




}
   