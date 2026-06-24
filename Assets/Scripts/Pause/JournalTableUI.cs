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
        Debug.Log("JournalTableUI Instance set");
    }
   /* private void Start()
    {
        // For testing, add all test documents to the journal at start
        Debug.Log("JournalTableUI Start: Adding test documents to journal");
        foreach (DocumentItem doc in testDocuments)
        {
            if (doc != null && doc.forceUnlock)
            {
                Debug.Log("Force unlocking document: " + doc.documentTitle);
                CollectDocument(doc);
            }
        }
    }*/


    public void CollectDocument(DocumentItem item)
    {
        Debug.Log("CollectDocument called for: " + item.documentTitle);

        if (documentItems.Contains(item))
        {
            return; // Already collected
        }

        //item.isUnlocked = true;

        item.pageNumber = documentItems.Count + 1; //assigns page number

        documentItems.Add(item);

        Debug.Log("Creating button for document: " + item.documentTitle);
        DocItemButton newButton = Instantiate(tocButtonPrefab, contentParent);

        newButton.Setup(item, pauseMenu);
    }

}
