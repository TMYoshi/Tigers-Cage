using UnityEngine;

public class InventoryItem : Item
{
    // [SerializeField]
    // private string itemName;
    // [SerializeField]
    // private int quantity;
    // [SerializeField]
    // private Sprite sprite;
    // [SerializeField]
    // private AudioClip audio_clip_;
    [SerializeField]
    private ItemSO item_so_;

    [TextArea]
    [SerializeField]
    private string itemDescription;
    private InventoryManager inventoryManager;

    public string ItemName => item_so_.item_name_;
    public int Quantity => item_so_.quantity_;
    public Sprite Sprite => item_so_.sprite_;
    public string ItemDescription => itemDescription;
    public AudioClip AudioClip => item_so_.audio_clip_;

    [SerializeField] private bool collectable;
    public bool Collectable => collectable;

    [SerializeField] private SpecialItems SpecialEvents;
    public SpecialItems GetSpecialEvents() => SpecialEvents;
    public void AssignSpecialEvents(SpecialItems specialEvent) => SpecialEvents = specialEvent;

    void Start()
    {
        Debug.Log("InventoryItem script started on " + gameObject.name);

        string itemId = GenerateItemId();
        if (InventoryManager.IsItemCollected(itemId))
        {
            Debug.Log($"Item {item_so_.item_name_} (ID: {itemId}) already collected, destroying");
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
        return sceneName + "_" + gameObject.name + "_" + item_so_.item_name_;
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