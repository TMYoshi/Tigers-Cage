using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{
    // item data public for debugging
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    // item slot
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;


    // item description slot
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    /*
    public GameObject selectedShader;
    */
    public bool thisItemSelected;

    private InventoryManager inventoryManager;
    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        /*
            if (selectedShader != null)
            {
                selectedShader.SetActive(false); // ensure selected panel starts as off
            }
        */
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        itemImage.color = new Color(1, 1, 1, 1);
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString(); // text component of TMP = int
        //quantityText.enabled = true; // applicable for items with quantity: coins etc, i assume most 
        // interactables will be single use however so disabling for time being
        itemImage.sprite = itemSprite;
    }

    public void RemoveItem()
    {
        this.itemName = null;
        this.quantity = 0;
        this.itemSprite = null;
        this.itemDescription = null;
        isFull = false;

        //sets color to transparent to avoid white null image
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0);
    }

    /*
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // m1
        {
            OnLeftClick();       
        }
        if (eventData.button == PointerEventData.InputButton.Right) // m2
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        if (inventoryManager != null)
        {
            inventoryManager.DeselectAllSlots();
        }

        if (selectedShader != null)
        {
            selectedShader.SetActive(true);
            //Debug.Log($"Activated selectedShader: {selectedShader.name}");
        }
        thisItemSelected = true;

        ItemDescriptionNameText.text = itemName; // remember .text
        //Debug.Log("Item Description: '" + itemDescription + "'");
        //Debug.Log("ItemDescriptionText is null: " + (ItemDescriptionText == null));

        if (ItemDescriptionText != null)
        {
            ItemDescriptionText.text = itemDescription;
            //Debug.Log("Set description text to: '" + ItemDescriptionText.text + "'");
        }
        else
        {
            Debug.LogError("ItemDescriptionText is not assigned in inspector!");
        }

        itemDescriptionImage.sprite = itemSprite;
        if(itemDescriptionImage.sprite == null)
        {
            itemDescriptionImage.sprite = emptySprite;
        }
    }

    // Lowkey thinking of making right click auto equip for quick access - Solivan
    public void OnRightClick()
    {
        Debug.Log("right clicked!");
    }
    */
}
