using UnityEngine;
using System.Collections.Generic;

public class JournalTableUI : MonoBehaviour
{
    [Header("Testing")]
    public DocumentItem[] testDocuments; // For testing purposes, assign in inspector
    public static JournalTableUI Instance;
    
    public Transform contentParent;
    public DocItemButton tocButtonPrefab;
    public PauseMenu pauseMenu;

    private List<DocumentItem> documentItems = new List<DocumentItem>();


    private void Awake()
    {
        Instance = this;
    }

    public void CollectDocument(DocumentItem item)
    {
        if (item.isUnlocked || item.forceUnlock)
        {
            return; // Already collected
        }

        item.isUnlocked = true;

        item.pageNumber = documentItems.Count + 1; //assigns page number

        documentItems.Add(item);

        DocItemButton newButton = Instantiate(tocButtonPrefab, contentParent);

        newButton.Setup(item, pauseMenu);
    }

    private void Start()
    {
        // For testing, add all test documents to the journal at start
        foreach (DocumentItem doc in testDocuments)
        {
            CollectDocument(doc);
        }
    }
}
