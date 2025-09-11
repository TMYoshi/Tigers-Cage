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
                if (hit.collider.gameObject.CompareTag("CollectibleItem"))
                {
                    InventoryItem inventoryItem = hit.collider.gameObject.GetComponent<InventoryItem>();

                    if (inventoryItem != null)
                    {
                        Debug.Log("Hit THIS Object!");

                        _context._InventoryManager.AddItem(
                            inventoryItem.ItemName,
                            inventoryItem.Quantity,
                            inventoryItem.Sprite,
                            inventoryItem.ItemDescription
                        );

                        Debug.Log($"Collected: {inventoryItem.ItemName} x{inventoryItem.Quantity}");
                    }
                    Object.Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                Debug.Log("No hit detected");
            }
        }
    }
}


