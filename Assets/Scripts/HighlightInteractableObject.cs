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
    [Header("Outline Settings")]
    public Color outlineColor = Color.black; // change later based on sprite
    public float outlineWidth = 0.1f;
    [Range(4, 16)]
    /* some sprites play well with this method, some don't; may need to revisit 
    unsure what balance to strike between resolution, outlineThickness for performance
    since it creates all the copies on startup */

    public int outlineResolution = 8; // how many outline copies to create

    private SpriteRenderer spriteRenderer;
    private GameObject outlineParent;

    void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        CreateOutlineEffect();
    }

    void CreateOutlineEffect()
    {
        // create a parent object to hold created outline sprites
        outlineParent = new GameObject(gameObject.name + "_OutlineGroup");
        outlineParent.transform.SetParent(transform); // makes outline parent a child of this GameObject
        outlineParent.transform.localPosition = Vector3.zero; // sets pos to (0,0,0) relative to parent
        outlineParent.transform.localScale = Vector3.one; // sets scale to (1,1,1)
        outlineParent.SetActive(false); // start hidden

        for (int i = 0; i < outlineResolution; i++)
        {
            float angle = (360f / outlineResolution) * i;
            Vector3 direction = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad), // x = cos
                Mathf.Sin(angle * Mathf.Deg2Rad), // y = sin
                0 // no z in 2d :)
            ) * outlineWidth;

            direction = transform.rotation * direction;

            // create outline sprite GameObject
            GameObject outlineSprite = new GameObject($"Outline_{i}");
            outlineSprite.transform.SetParent(outlineParent.transform);
            outlineSprite.transform.localPosition = direction;
            outlineSprite.transform.localScale = Vector3.one;

            outlineSprite.transform.rotation = transform.rotation;

            // add and config SpriteRenderer
            SpriteRenderer outlineSR = outlineSprite.AddComponent<SpriteRenderer>();
            outlineSR.sprite = spriteRenderer.sprite;
            outlineSR.color = outlineColor;

            // sort and re-layer 
            outlineSR.sortingLayerName = spriteRenderer.sortingLayerName;
            outlineSR.sortingOrder = spriteRenderer.sortingOrder - 1;
        }

        //Debug.Log($"Created outline effect with {outlineResolution} sprites.");
    }

    public void Enter()
    {
        // show outline
        if (outlineParent != null)
        {
            outlineParent.SetActive(true);
        }
        Debug.Log("Mouse entered " + gameObject.name);
    }

    public void Exit()
    {
        // hide outline
        if (outlineParent != null)
        {
            outlineParent.SetActive(false);
        }
    }
    void OnDestroy()
    {
        // clean up outlines
        if (outlineParent != null)
        {
            DestroyImmediate(outlineParent);
        }
    }
}
