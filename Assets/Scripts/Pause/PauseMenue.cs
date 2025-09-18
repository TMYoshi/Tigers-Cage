using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject JournalUI;
    private bool isPaused = false;
    void Start()
    {
        if (JournalUI != null)
        {
            JournalUI.SetActive(false);
        }
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
    }

    public void options()
    {
        Debug.Log("options opened");
        //Todo:show your options UI
    }

    public void Save()
    {
        Debug.Log("Game Saves");
        Debug.Log("Game Saves2");
        //Todo: call save
    }

    public void QuitGame()
    {
        Debug.Log("Gamequit");
        Application.Quit();
        Debug.Log("Gamequit part 2");
    }
}
