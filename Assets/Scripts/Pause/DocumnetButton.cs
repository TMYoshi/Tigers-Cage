using UnityEngine;

public class DocumnetButton : MonoBehaviour
{
    public DocumentItem documentItem;
    public PauseMenu pauseMenu;

    public void open()
    {
        
        Debug.Log("Document button clicked: " + gameObject.name);
        
       if(documentItem == null)
        {
            Debug.LogWarning("DocumentItem is not assigned for " + gameObject.name);
            return;
        }

        if(pauseMenu == null)
        {
            Debug.LogWarning("PauseMenu reference is not assigned for " + gameObject.name);
            return;
        }



        pauseMenu.OpenDocumentByItem(documentItem);
    }

}
