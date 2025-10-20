/* HighlightInteractableOutline.cs
 * Creates an outline based on specified color, outline thickness, and resolution; attach this to any game object
 * with a sprite renderer to have a highlight effect when mouse hovers over the item, and stops highlighting
 * when mouse moves away from the item.
 * 
 * Creates an empty GameObject outlineParent to hold all the duplicate sprites (count is based on resolution), 
 * and places these based on 360�/resolution
 * 
 * Most of these calculations were created with trigonomety to calculate offsets, whose vector is then 
 * multiplied by outlineWidth to determine how far away from center the duplicate is placed.
 * 
 * Notes: Ensure GameObjects that you intend to interact with are on a layer visible to camera and have a 2D box
 * collider.
 */
using UnityEngine;

public class HighlightInteractableOutline : MonoBehaviour
{
    [SerializeField] Material highlight, nonHighlight;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color outlineColor = Color.black;
    [SerializeField] private float outlineSize = 0.01f;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Enter()
    {
        highlight.SetColor("_OutlineColor", outlineColor);
        highlight.SetFloat("_Size", outlineSize);
        spriteRenderer.material = highlight;
    }

    public void Exit()
    {
        spriteRenderer.material = nonHighlight;
    }
    void OnDestroy()
    {
        spriteRenderer.material = nonHighlight;
    }
}
