using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DocumentItemButton : MonoBehaviour
{
    public TMP_Text PageNumberText;
    public TMP_Text documentTitleText;

    private DocumentItem documentItem;
    private PauseMenu pauseMenu;

    public void Setup(DocumentItem item, PauseMenu menu, uint page)
    {
        documentItem = item;
        pauseMenu = menu;

        PageNumberText.text = $"page {page} ...............";
        documentTitleText.text = item.documentTitle;
        
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OpenDocument);
    }

    private void OpenDocument()
    {
        pauseMenu.OpenDocumentByItem(documentItem);
    }
}
