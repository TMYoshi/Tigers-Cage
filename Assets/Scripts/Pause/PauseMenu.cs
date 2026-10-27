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
    [SerializeField] GameObject mainPanel;// Journal Panel with three buttons
    [SerializeField] GameObject tableOfContents; //Second UI 
    [SerializeField] GameObject settingsPanel; // Options menu panel
    [SerializeField] GameObject documentPage;
    
    [Header("Page layout")]
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text contentText;
    [SerializeField] Image documentImage;

    [Header("Animators")]
    [SerializeField] Animator journalAnimator;

    public bool IsPaused => isPaused;
    private bool isPaused = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public static void InvHandler()
    {
        if(!Instance) return;

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

    void TurnOffAllPages()
    {
        settingsPanel.SetActive(false);
        documentPage.SetActive(false);
        tableOfContents.SetActive(false);
        mainPanel.SetActive(false);
    }

    void Start()
    {
        TurnOffAllPages();
    }

    public void PauseGame()
    {
        isPaused = true;
        PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Null);
        journalAnimator.SetBool("Show", true);
        TurnOffAllPages();
        mainPanel.SetActive(true);

        Debug.Log("games is paused ");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Debug.Log("games is unpaused ");
        PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Idle);
        journalAnimator.SetBool("Show", false);
    }

    public void OpenTable()
    {
        TurnOffAllPages();
        tableOfContents.SetActive(true);
    }

    public void PauseUI()
    {
        TurnOffAllPages();
        mainPanel.SetActive(true);
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

        TurnOffAllPages();
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

    public void OpenSettings()
    {
        TurnOffAllPages();
        settingsPanel.SetActive(true);
    }
}
   
