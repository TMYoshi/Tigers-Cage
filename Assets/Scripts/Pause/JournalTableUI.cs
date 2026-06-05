using UnityEngine;
using System.Collections.Generic;

public class JournalTableUI : MonoBehaviour
{
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
        if (item.isUnlocked)
        {
            return; // Already collected
        }

        item.isUnlocked = true;

        item.pageNumber = documentItems.Count + 1; //assigns page number

        documentItems.Add(item);

        DocItemButton newButton = Instantiate(tocButtonPrefab, contentParent);

        newButton.Setup(item, pauseMenu);
    }
}
