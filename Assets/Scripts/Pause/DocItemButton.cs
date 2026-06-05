using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DocItemButton : MonoBehaviour
{
    public TMP_Text label;
    public Button button;

    private DocumentItem documentItem;
    private PauseMenu pauseMenu;

    public void Setup(DocumentItem item, PauseMenu menu)
    {
        documentItem = item;
        pauseMenu = menu;
        label.text = $"page {item.pageNumber}.......{item.documentTitle}";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OpenDocument);
    }

    private void OpenDocument()
    {
        if (documentItem.isUnlocked)
        {
          // pauseMenu.OpenDocument(documentItem);
        }
    }
}
