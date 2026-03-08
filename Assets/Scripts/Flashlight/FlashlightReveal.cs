using UnityEngine;

public class FlashlightReveal : MonoBehaviour
{
    [Header("References")]
    public FlashlightController _flashlight;
    private SpriteRenderer _spriteRenderer;

    [Header("Settings")]
    [SerializeField] private float revealRadius = 2.5f;
    [SerializeField] private Color darkColor = new Color(0.12f, 0.12f, 0.12f);
    [SerializeField] private Color brightColor = Color.white;

    private float t; // lerp progress
    public float GetCurrentRevealValue() => t;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _flashlight = Object.FindFirstObjectByType<FlashlightController>();

        if (_spriteRenderer != null) _spriteRenderer.color = darkColor;
    }

    void Update()
    {
        if (_flashlight == null || _spriteRenderer == null) return;

        if (_flashlight.isFlashlightOn)
        {
            float distance = Vector2.Distance(transform.position, _flashlight.flashlightObject.transform.position);
            t = 1f - Mathf.Clamp01(distance / revealRadius);
            //Debug.Log("t is " + t);
            _spriteRenderer.color = Color.Lerp(darkColor, brightColor, t);
            //Debug.Log("color is " + _spriteRenderer.color);

        }
        else { 
            _spriteRenderer.color = darkColor;
            t = 0;
        }
    }
}
