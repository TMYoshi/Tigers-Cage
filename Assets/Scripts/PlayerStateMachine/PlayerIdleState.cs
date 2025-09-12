using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateManager context) : base(context)
    {
        _context = context;
    }
    public override void EnterState()
    {

    }
    public override void UpdateState()
    {
        //this state just listens for inputs from the player
        if (Input.GetButtonDown("Inventory"))
        {
            _context.UpdateCurrentState(PlayerStateManager.State.Inventory);
        }

        // checks for clicking on interactables every frame
        ClickItem();
    }
    public override void ExitState()
    {
        Debug.Log("Exited idle state.");
    }


    public void ClickItem()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Debug.Log("Mouse clicked somewhere");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                InventoryItem _inventoryItem = hit.collider.gameObject.GetComponent<InventoryItem>();
                switch (hit.collider.gameObject.tag)
                {
                    case "Item":
                        Debug.Log("Hit non-collectible item");
                        break;
                    case "CollectibleItem":
                        if (_inventoryItem.WriteLines())
                        {
                            AddItemToInv(_inventoryItem);
                            Object.Destroy(hit.transform.gameObject);
                        }
                        else
                        {
                            _inventoryItem.WriteLines();
                        }
                        break;
                    default:
                        Debug.Log("Hit non-item object: " + hit.collider.gameObject.name);
                        break;
                }
            }
        }
    }
    void AddItemToInv(InventoryItem _inventoryItem)
    {
        _context._InventoryManager.AddItem(
            _inventoryItem.ItemName,
            _inventoryItem.Quantity,
            _inventoryItem.Sprite,
            _inventoryItem.ItemDescription
        );
    }
}


