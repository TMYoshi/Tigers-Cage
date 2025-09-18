using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] private bool collectable;
    public bool Collectable => collectable;

    private SpecialItems SpecialEvents;
    public SpecialItems GetSpecialEvents() => SpecialEvents;
    public void AssignSpecialEvents(SpecialItems specialEvent) => SpecialEvents = specialEvent;

    void Start()
    {
        Debug.Log("InventoryItem script started on " + gameObject.name);

        string itemId = GenerateItemId();
        if (InventoryManager.IsItemCollected(itemId))
        {
            Debug.Log($"Item {itemName} (ID: {itemId}) already collected, destroying");
            Destroy(gameObject);
            return;
        }

        //inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        //if (inventoryManager == null)
        //{
        //    Debug.LogError("Error: InventoryManager not found in scene. Ensure it is active and has script attached.");
        //}
    }
    private string GenerateItemId()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        return sceneName + "_" + gameObject.name + "_" + itemName;
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