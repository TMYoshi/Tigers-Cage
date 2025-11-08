using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class CogController : MonoBehaviour
{
    // each cog has inner and outer collider; if outer collider (teeth) overlap with 
    // inner collider (inner circumference) 
    public enum CogSize { Small, Medium, Large }
    public CogSize size;

    public Collider2D innerCollider;
    public Collider2D outerCollider;

    private List<Collider2D> overlapResults = new List<Collider2D>();

    private Collider2D[] allColliders;

    private Vector3 currentAxlePosition = Vector3.zero;
    private Vector3 trayScale;
    private Vector3 onBoardScale;
    private Vector3 initialTrayPosition;

    private Rigidbody2D rb;

    private bool isDragging = false;
    private bool isInitialized = false;
    //private bool canStartDrag = false;

    // initialized via CogInitializer
    public void SetInitialProperties(Vector3 scale, Vector3 position)
    {
        trayScale = scale;
        initialTrayPosition = position;

        float targetOnBoardMagnitude = CogInitializer.CogScales.GetHoverScale(size);
        onBoardScale = Vector3.one * targetOnBoardMagnitude;

        isInitialized = true;
    }

    private void Awake()
    {
        allColliders = GetComponents<Collider2D>();
        if (allColliders.Length < 2 || innerCollider == null || outerCollider == null)
        {
            Debug.LogError($"Cog {gameObject.name} needs 2 Collider2D components: one inner, one outer, and both must be assigned in the Inspector.");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0;
        }

        if (!isInitialized)
        {
            SetInitialProperties(transform.localScale, transform.position);
        }
    }

    private void Update()
    {
        int draggableLayerMask = ~(1 << LayerMask.NameToLayer("FixedCogs")); // ~ to exclude FixedCogs layer from check

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, draggableLayerMask);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                //canStartDrag = true;
                HandleMouseDown();
            }
        }

        if (isDragging)
        {
            HandleMouseDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                HandleMouseUp();
            }
            //canStartDrag = false;
        }

        // if: check for connection (invalid if outer overlaps with inner) 
        HandleCogRotation();
    }

    private void HandleMouseDown()
    {
        if (!isInitialized) return;

        // release current position if any
        if (currentAxlePosition != Vector3.zero && AxleManager.Instance != null)
        {
            AxleManager.Instance.ReleasePosition(currentAxlePosition);
            currentAxlePosition = Vector3.zero;
        }

        isDragging = true;

        // scale up to hover size (sizes found in CogInitializer)
        transform.localScale = onBoardScale;
    }

    private void HandleMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = transform.position.z; // keep z constant in 2D

            transform.position = mouseWorldPosition;
        }
    }

    private void HandleMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            if (AxleManager.Instance == null)
            {
                Debug.LogError("AxleManager is not initialized. Cog cannot snap.");
                SnapBackToTray();
                return;
            }

            Vector3 releaseLocalPosition = AxleManager.Instance.transform.InverseTransformPoint(transform.position);
            Vector3 snapPosition = AxleManager.Instance.FindNearestAvailablePosition(releaseLocalPosition);

            if (snapPosition != Vector3.zero)
            {
                Vector3 snapWorldPosition = AxleManager.Instance.transform.TransformPoint(snapPosition);
                transform.position = snapWorldPosition;

                // hard fail: cog teeth overlap with the inner radius of another cog
                if (CheckForInvalidOverlap())
                {
                    Debug.Log("Invalid placement; inner circumference overlaps another cog's teeth. Returning to tray.");
                    SnapBackToTray();
                    return;
                }

                // soft fail: cog teeth do not overlap with another cog's teeth (for checking for rotation)
                if (!CheckForValidMeshing())
                {
                    Debug.Log("Cog placed but unmeshed with adjacent cog.");
                }

                // successful snap
                currentAxlePosition = snapPosition;
                AxleManager.Instance.OccupyPosition(currentAxlePosition, this);
            }
            else
            {
                // snap failed
                Debug.Log("Snap failed; returning cog to tray position.");
                SnapBackToTray();
            }
        }
    }

    private void SnapBackToTray()
    {
        transform.localScale = trayScale;
        transform.position = initialTrayPosition;
        currentAxlePosition = Vector3.zero;
    }


    public bool CheckForInvalidOverlap()
    {
        if (innerCollider == null || outerCollider == null) return false;

        ContactFilter2D filter = ContactFilter2D.noFilter;
        int count = innerCollider.Overlap(filter, overlapResults);

        if (count > 0)
        {
            foreach (Collider2D otherCollider in overlapResults)
            {
                if (otherCollider == innerCollider || otherCollider == outerCollider) continue;
                CogController otherCog = otherCollider.GetComponent<CogController>();

                if (otherCog != null && otherCollider == otherCog.outerCollider)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckForValidMeshing()
    {
        if (innerCollider == null || outerCollider == null) return false;

        ContactFilter2D filter = ContactFilter2D.noFilter;
        int count = outerCollider.Overlap(filter, overlapResults);

        if (count > 0)
        {
            foreach (Collider2D otherCollider in overlapResults)
            {
                if (otherCollider == innerCollider || otherCollider == outerCollider) continue;

                CogController otherCog = otherCollider.GetComponent<CogController>();

                if (otherCog != null)
                {
                    if (otherCollider == otherCog.outerCollider)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void HandleCogRotation()
    {
        if (name.StartsWith("cog_small_fixed_start"))
        {
            transform.Rotate(0, 0, -60 * Time.deltaTime);
        }
    }
}