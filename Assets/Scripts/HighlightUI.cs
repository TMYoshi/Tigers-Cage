using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class HighlightUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private Color originalColor;

    [SerializeField] private Color highlightColor = Color.white;
    [SerializeField] private float highlightScale = 1.1f;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        if(buttonImage != null)
        {
            originalColor = buttonImage.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.color = highlightColor;
            transform.localScale = Vector3.one * highlightScale;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.color = originalColor;
            transform.localScale = Vector3.one;
        }
    }
}