using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CogController : MonoBehaviour
{
    public enum CogSize { Small, Medium, Large }
    public CogSize size;

    private Vector3 currentAxlePosition = Vector3.zero;
    private Vector3 originalScale;
    private Vector3 initialTrayPosition; // to snap back during invalid axle placements

    private Rigidbody2D rb;

    private bool isDragging = false;
    private bool isInitialized = false;

    private bool canStartDrag = false;
    public void SetInitialProperties(Vector3 scale, Vector3 position)
    {
        originalScale = scale;
        initialTrayPosition = position;
        isInitialized = true;
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
            Debug.LogWarning($"{gameObject.name} was not explicitly initialized with CogInitializer. Using current transform as fallback.");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                canStartDrag = true;
                HandleMouseDown();
            }
        }

        if (isDragging)
        {
            HandleMouseDrag();
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
            canStartDrag = false;
        }

        if (Input.GetMouseButtonUp(0) && !isDragging)
        {
            canStartDrag = false;
        }
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
        float targetHoverScaleMagnitude = CogInitializer.CogScales.GetHoverScale(size);
        float scaleFactor = targetHoverScaleMagnitude / originalScale.x;

        transform.localScale = originalScale * scaleFactor;
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
                transform.localScale = originalScale;
                transform.position = initialTrayPosition;
                return;
            }

            Vector3 releaseLocalPosition = AxleManager.Instance.transform.InverseTransformPoint(transform.position);

            Vector3 snapPosition = AxleManager.Instance.FindNearestAvailablePosition(releaseLocalPosition);

            if (snapPosition != Vector3.zero)
            {
                // successful snap; scale maintained
                Vector3 snapWorldPosition = AxleManager.Instance.transform.TransformPoint(snapPosition);

                transform.position = snapWorldPosition;

                currentAxlePosition = snapPosition;
                AxleManager.Instance.OccupyPosition(currentAxlePosition, this);

                // TODO: check for cog connections 
            }
            else
            {
                // snap failed
                Debug.Log("Snap falied; returning cog to tray position.");
                transform.localScale = originalScale;
                transform.position = initialTrayPosition;

                currentAxlePosition = Vector3.zero;
            }
        }
    }
}
