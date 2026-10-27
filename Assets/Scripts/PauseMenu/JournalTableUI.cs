using UnityEngine;
using System.Collections.Generic;

public class JournalTableUI : MonoBehaviour
{
    public static JournalTableUI Instance;
    
    public Transform contentParent;
    public GameObject buttonObject;
    public PauseMenu pauseMenu;

    public static List<DocumentItem> DocumentItems = new List<DocumentItem>();

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

    private void Start()
    {
        foreach (string _itemName in SaveData.Instance.UnlockedDocuments)
        {
            DocumentItem item = MasterList.Instance.EveryDocumentItem
                                    [MasterList.Instance.EveryDocumentItem.FindIndex(item => item.documentTitle == _itemName)];
            GenerateDocument(item);
        }
    }

    public static void CollectDocument(DocumentItem _item)
    {
        Debug.Log("CollectDocument called for: " + _item.documentTitle);
        if(SaveData.Instance.UnlockedDocuments.Contains(_item.documentTitle))
            return;

        SaveData.Instance.UnlockedDocuments.Add(_item.documentTitle);
        SaveData.Instance.SavePlayer();
        
        Instance.GenerateDocument(_item);
    }

    public void GenerateDocument(DocumentItem _item)
    {
        if (DocumentItems.Contains(_item))
            return; // Already collected

        DocumentItems.Add(_item);

        Debug.Log("Creating button for document: " + _item.documentTitle);
        
        DocumentItemButton newButton = Instantiate(buttonObject, contentParent).GetComponent<DocumentItemButton>();

        newButton.Setup(_item, pauseMenu, (uint)(DocumentItems.Count));
    }
}
