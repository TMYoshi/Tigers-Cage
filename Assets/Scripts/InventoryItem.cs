using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryItem : Item
{
    [SerializeField]
    private string itemName;
    [SerializeField]
    private int quantity;
    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManager inventoryManager;

    public string ItemName => itemName;
    public int Quantity => quantity;
    public Sprite Sprite => sprite;
    public string ItemDescription => itemDescription;

    void Start()
    {
        Debug.Log("InventoryItem script started on " + gameObject.name);
        //inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        //if (inventoryManager == null)
        //{
        //    Debug.LogError("Error: InventoryManager not found in scene. Ensure it is active and has script attached.");
        //}
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0)) // Left mouse button
    //    {
    //        Debug.Log("Mouse clicked somewhere");
    //        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

    //        if (hit.collider != null)
    //        {
    //            Debug.Log("Hit object: " + hit.collider.gameObject.name);
    //            if (hit.collider.gameObject == gameObject)
    //            {
    //                Debug.Log("Hit THIS object!");
    //                // click logic here
    //                if (inventoryManager != null)
    //                {
    //                    inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
    //                    Debug.Log("Item '" + itemName + "' (" + quantity + ") collected!");
    //                }
    //                Destroy(gameObject);
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("No hit detected");
    //        }
    //    }
    //}
}