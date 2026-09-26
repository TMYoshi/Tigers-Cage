using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DocItemButton : MonoBehaviour
{
    public TMP_Text PageNumberText;
    public TMP_Text documentTitleText;
    public Button button;

    private DocumentItem documentItem;
    private PauseMenu pauseMenu;

    public void Setup(DocumentItem item, PauseMenu menu)
    {
        documentItem = item;
        pauseMenu = menu;

        PageNumberText.text = $"page {item.pageNumber}...............";
        documentTitleText.text = item.documentTitle;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OpenDocument);
    }

    private void OpenDocument()
    {
        if (documentItem.isUnlocked)
        {
          pauseMenu.OpenDocumentByItem(documentItem);
        }
    }
}
