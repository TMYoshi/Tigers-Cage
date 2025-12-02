using UnityEngine;

public class JournalDataManager : MonoBehaviour
{
    //Summary: Manaages all journal doucment data using a scritableObjects
    public static JournalDataManager Instance; //other scripts accesss this manager

    [Header("All Document Data (ScriptableObjects)")]
    public DocumentItem[] allDocuments;

    void Awake()
    {
        //allows one manager exists between scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);// if it exist it will destroy the copy
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        //Data stays between the scenes 
    }

    void Start()
    {
        LoadProgress();
        //UnlockDocument(0); test the jounral documents by uncomment
        
    }

    public void UnlockDocument(int index)
    {
        if (index < 0 || index >= allDocuments.Length)
        {
            Debug.LogWarning("Invalid document index: " + index);
            return;
        }
        //Mark the doucments as unlock
        allDocuments[index].isUnlocked = true;
        SaveProgress();
    }

    public void SaveProgress()
    {
        for (int i = 0; i < allDocuments.Length; i++)
        {
            PlayerPrefs.SetInt("DocUnlocked_" + i, allDocuments[i].isUnlocked ? 1 : 0);
        }

        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        for (int i = 0; i < allDocuments.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("DocUnlocked_" + i, 0) == 1;
            allDocuments[i].isUnlocked = unlocked;
        }
    }


}
