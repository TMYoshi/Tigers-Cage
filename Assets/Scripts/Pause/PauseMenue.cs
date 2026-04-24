using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;


public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Save/Load")]
    public SaveLoad saveLoadManager;

    [Header("UI Refrences")]
    public GameObject JournalUI;// pause menu panel first
    public GameObject PauseBackground;// Journal Panel with three buttons
    public GameObject tableofContentes; //Second UI 

    public GameObject SettingsPanel; // Options menu panel
    public TMP_Text titleText;
    public TMP_Text contentText;
    public Image documentImage;

    public GameObject documentPage;

    [Header("TOC Buttons")]
    public Button[] documentButtons;

    public static bool isPaused = false;
    private Action invHandler;

    void Start()
    {

        RefreshButtons();
        invHandler = () =>
        {
            if(!this || JournalUI == null) return;

            if(isPaused)
                ResumeGame();
            else
                PauseGame();
        };
        PlayerInput.Instance.InvOnClick += invHandler;
        PauseBackground.SetActive(false);
        JournalUI.SetActive(false);


    }
    //Check if the key 'J" is pressed. it will pause the game
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
        RefreshButtons();
        Debug.Log("Opened table of contetnents");
        tableofContentes.SetActive(true);

    }

   public void BackToTable()
{
    Debug.Log("BackToTable button clicked");

    if (documentPage == null) Debug.LogError("documentPage is NOT assigned!");
    if (tableofContentes == null) Debug.LogError("tableofContentes is NOT assigned!");

    documentPage.SetActive(false);
    tableofContentes.SetActive(true);
    RefreshButtons();
}
   /* void UpdateTOCButtons()
    {
        if(JournalDataManager.Instance == null) return;
        var data = JournalDataManager.Instance.allDocuments;

        for (int i = 0; i < documentButtons.Length; i++)
        {
            bool unlocked = data[i].isUnlocked;
            documentButtons[i].gameObject.SetActive(unlocked);

            int index = i; // local copy for lambda
            documentButtons[i].onClick.RemoveAllListeners();
            documentButtons[i].onClick.AddListener(() => OpenDocument(index));
        }
    }*/

    public void RefreshButtons()
    {
        foreach(Button but in documentButtons)
        {
            DocumnetButton docBut = but.GetComponent<DocumnetButton>();

            if(docBut != null && docBut.documentItem != null)
            {
                but.gameObject.SetActive(docBut.documentItem.isUnlocked);
            }
        }
    }

    public void OpenDocumentByItem(DocumentItem doc)
    {
       if(doc == null) return;

        titleText.text = doc.documentTitle;
        contentText.text = doc.documentText;
        documentImage.sprite = doc.documentImage;

        if(doc.documentInfoFont != null)
        {
            titleText.font = doc.documentInfoFont;
            contentText.font = doc.documentInfoFont;
        }

        tableofContentes.SetActive(false);
        documentPage.SetActive(true);

        Debug.Log($"Opened document: {doc.documentTitle}");
    }

    public void OpenDocument(int index)
    {
        var doc = JournalDataManager.Instance.allDocuments[index];
        titleText.text = doc.documentTitle;
        contentText.text = doc.documentText;
        documentImage.sprite = doc.documentImage;

        //applying font if set
        if(doc.documentInfoFont != null)
        {
            titleText.font = doc.documentInfoFont;
            contentText.font = doc.documentInfoFont;
        }
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

    public void Options()
    {
        // Implement options menu logic here
        Debug.Log("Options menu opened");
        SettingsPanel.SetActive(true);

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

    void Awake()
    {
    
        Time.timeScale = 1f;
        isPaused = false;
        JournalUI.SetActive(false);
        PauseBackground.SetActive(false);
        tableofContentes.SetActive(false);
        documentPage.SetActive(false);
        Debug.Log("Awake to function to reset Journal has been done");
    }




}
   
