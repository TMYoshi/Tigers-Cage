using UnityEngine;

public class JournalUI : MonoBehaviour
{
    public GameObject journalPanel;
    private bool IsOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Journal"))
        {
            ToggleJournal();
        }
    }

    public void ToggleJournal()
    {
        IsOpen = !IsOpen;
        journalPanel.SetActive(IsOpen);

        Time.timeScale = IsOpen ? 0 : 1;

    }

    public void OpenInventory()
    {
        Debug.Log("Inventory Opened");
        //Need to open or replace call with a different function
    }

    public void OpenCharacterMenue()
    {
        Debug.Log("Character Menue Opened");
        //Open character screen
    }

    public void SaveGame()
    {
        Debug.Log("Game Saves");
        // Add save system logic from file 
    }
}
