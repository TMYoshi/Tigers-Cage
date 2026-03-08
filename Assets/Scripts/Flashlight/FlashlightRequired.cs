using UnityEngine;

public class FlashlightRequired : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FlashlightReveal _flashlightReveal;
    [SerializeField] private GameObject darkObject;
    [SerializeField] private GameObject lightObject;

    [Header("Swap Threshold")]
    [Range(0, 1)]
    [SerializeField] private float swapThreshold = 0.80f; // compared to lerp progress t

    private SpriteRenderer darkRenderer;
    private Collider2D darkCollider;

    void Start()
    {
        if (darkObject != null)
        {
            darkRenderer = darkObject.GetComponent<SpriteRenderer>();
            darkCollider = darkObject.GetComponent<Collider2D>();
        }
    }

    void Update()
    {
        if (lightObject == null)
        {
            SetDarkState(false);
            return;
        }

        if (_flashlightReveal == null) return;

        float currentReveal = _flashlightReveal.GetCurrentRevealValue();

        if (currentReveal >= swapThreshold)
        {
            SetDarkState(false);
            if (!lightObject.activeSelf) lightObject.SetActive(true);
        }
        else
        {
            SetDarkState(true);
            if (lightObject.activeSelf) lightObject.SetActive(false);
        }
    }
    void SetDarkState(bool active)
    {
        if (darkRenderer != null) darkRenderer.enabled = active;
        if (darkCollider != null) darkCollider.enabled = active;
    }
}
