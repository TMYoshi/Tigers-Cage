using UnityEngine;

public class JournalCollectable : MonoBehaviour
{
   public DocumentItem documentItem;
   public bool unlockedForTesting = false; // Set to true in inspector to unlock this item without collecting

     private void Start()
    {
        if (unlockedForTesting && documentItem != null)
        {
            AddtoJournal();
        }
    }

   public void AddtoJournal()
    {
        if(documentItem == null)
        {
            Debug.LogWarning("DocumentItem is not assigned for " + gameObject.name);
            return;
        }

        JournalTableUI.Instance.CollectDocument(documentItem);
        Debug.Log("Collected document: " + documentItem.documentTitle);
    }
}
