using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Overlays;
using Mono.Cecil.Cil;

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
        //LoadProgress();
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
        DocumentItem doc = allDocuments[index];

        doc.isUnlocked = true;
        SaveProgress();

        if(JournalTableUI.Instance != null)
        {
            JournalTableUI.Instance.CollectDocument(doc);
        }
    }

    public List<JournalPageSaveData> BuildJournalPageSaveData()
    {
        List<JournalPageSaveData> saveData = new List<JournalPageSaveData>();

        for(int i = 0; i < allDocuments.Length; i++)
        {
            DocumentItem doc = allDocuments[i];

            if(doc == null)
            {
                continue; // Skip null entries
            }

            if (doc.isUnlocked)
            {
                JournalPageSaveData pageData = 
                new JournalPageSaveData
                (doc.documentTitle, 
                doc.pageNumber);

                saveData.Add(pageData);
            }
        }
        return saveData;
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
