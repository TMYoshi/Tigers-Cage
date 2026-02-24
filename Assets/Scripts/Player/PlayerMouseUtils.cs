using System;
using UnityEditor;
using UnityEngine;

public class PlayerMouseUtils : MonoBehaviour
{
    HighlightInteractableOutline outlineScript;
    //this is for sprites
    public Collider2D HighlightOnHover()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            return hit.collider;
        }

        if (hit.collider != null)
        {
            HighlightInteractableOutline newOutline = hit.collider.gameObject.GetComponent<HighlightInteractableOutline>();
            if (outlineScript == newOutline) return null;
            if (outlineScript != null) outlineScript.Exit();

            outlineScript = newOutline;

            if (outlineScript != null) outlineScript.Enter();
        }
        else
        {
            // Not hovering anything, exit previous outline
            if (outlineScript == null) return null;

            outlineScript.Exit();
            outlineScript = null;
        }

        return null;
    }
}
