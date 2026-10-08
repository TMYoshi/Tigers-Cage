using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;


public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //[Header("Save/Load")]
    //public SaveLoad saveLoadManager;

    public static PauseMenu Instance;

    [Header("UI Refrences")]
    public GameObject JournalUI;// pause menu panel first
    public GameObject PauseBackground;// Journal Panel with three buttons
    public GameObject TableOfContents; //Second UI 
    //public GameObject PreFabTableOfContents; // Third UI

    public GameObject SettingsPanel; // Options menu panel
    public TMP_Text titleText;
    public TMP_Text contentText;
    public Image documentImage;

    public GameObject documentPage;

    [Header("TOC Buttons")]
    public Button[] documentButtons;

    private bool isPaused = false;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public static void InvHandler()
    {
        if(!Instance || Instance.JournalUI == null) return;

        if(Instance.isPaused)
        {
            PlayerInput.Instance.InvOnClick -= PauseMenu.InvHandler;
            Instance.ResumeGame();
        }
        else
        {
            PlayerInput.Instance.InvOnClick += PauseMenu.InvHandler;
            Instance.PauseGame();
        }
    }

    void Start()
    {

        RefreshButtons();
        PauseBackground?.SetActive(false);
        JournalUI.SetActive(false);
    }

    //Check if the key 'J" is pressed. it will pause the game
    //Game will freeze and show UI pause menue
    public void PauseGame()
    {
        isPaused = true;
        PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Null);
        JournalUI.SetActive(true);// shows pause menue
        documentPage.SetActive(false);
        TableOfContents.SetActive(false);
        SettingsPanel.SetActive(false);
        PauseBackground.SetActive(true);

        Debug.Log("games is paused ");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Debug.Log("games is unpaused ");
        PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Idle);
        JournalUI.SetActive(false);
        documentPage.SetActive(false);
    }

    public void OpenJournal()
    {
        PauseBackground.SetActive(false);
        RefreshButtons();
        Debug.Log("Opened Table");
        JournalTableUI.Instance.RefreshTable();
        TableOfContents.SetActive(true);

    }

   public void BackToTable()
    {
        Debug.Log("BackToTable button clicked");

        documentPage.SetActive(false);

        TableOfContents.SetActive(true);
        RefreshButtons();
    }

    public void  PauseUI()
    {
        Debug.Log("BackToPauseMenu button clicked");
        TableOfContents.SetActive(false);
        SettingsPanel.SetActive(false);
        PauseBackground.SetActive(true);
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

        TableOfContents.SetActive(false);
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
        TableOfContents.SetActive(false);
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
}
   
