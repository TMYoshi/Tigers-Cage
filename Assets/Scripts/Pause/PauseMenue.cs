using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject JournalUI;
    public GameObject DocumnetsPanel;
    public GameObject PauseBackground;
    public GameObject[] DocumentPages;
    private bool isPaused = false;
    private int currentPage = 0;

    void Start()
    {
        if (JournalUI != null)
        {
            JournalUI.SetActive(false);
        }

        if (DocumnetsPanel != null) PauseBackground.SetActive(true);
        if (DocumnetsPanel != null) DocumnetsPanel.SetActive(false);

        ShowPage(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Journal"))
        {
            Debug.Log("Pause Start");
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (JournalUI == null)
        {
            return;
        }
        else
        {
            isPaused = !isPaused;
            JournalUI.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }

        if (isPaused)
        {
            ShowMainMenu();
        }
    }

    public void ShowDocumnets()
    {
        PauseBackground.SetActive(false);
        DocumnetsPanel.SetActive(true);
        ShowPage(0);
    }

    public void options()
    {
        Debug.Log("options opened");
    }

    public void ShowMainMenu()
    {
        Debug.Log("Main Menue opened");
         PauseBackground.SetActive(true);
         DocumnetsPanel.SetActive(false);
        
        //Todo:show your options UI
    }

    public void NextPage()
    {
        if (currentPage < DocumentPages.Length - 1)
        {
            ShowPage(currentPage + 1);
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            ShowPage(currentPage - 1);
        }
    }

    private void ShowPage(int index)
    {
        if (DocumentPages == null || DocumentPages.Length == 0) return;

        for (int i = 0; i < DocumentPages.Length; i++)
        {
            DocumentPages[i].SetActive(i == index);
        }

        currentPage = index;
    }

    public void Save()
    {
        Debug.Log("Game Saves");
        Debug.Log("Game Saves2");
        //Todo: call save
    }

   public void QuitToMainMenu()
{
    // Make sure time is running again
    Time.timeScale = 1f;

    // Load main menu directly
    SceneManager.LoadScene("Main Menu (Fish)");
}
}
