using System.Diagnostics;
using UnityEngine;

public class NeedsFlashlight : MonoBehaviour
{
    [Header("References")]
    public FlashlightController _flashlight;
    private SpriteRenderer _spriteRenderer;
    private InventoryItem _itemData;

    [Header("Settings")]
    [SerializeField] private float revealRadius = 2.5f;
    [SerializeField] private Color darkColor = new Color(0.12f, 0.12f, 0.12f);
    [SerializeField] private Color brightColor = Color.white;

    [Header("Dialogue SOs")]
    [SerializeField] private DialogSO noFlashlightSO; // unowned flashlight
    [SerializeField] private DialogSO darkFailureSO;         // flashlight owned, not on
    [SerializeField] private DialogSO lightSuccessSO;       // success
    // note: proximity/dist shouldnt matter since flashlight follows cursor and we click on items already
    // this keeps approach simpler: just check if light is on or not
   
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _itemData = GetComponent<InventoryItem>();
        _flashlight = Object.FindFirstObjectByType<FlashlightController>();

        if (_spriteRenderer != null) _spriteRenderer.color = darkColor;
    }

    void Update()
    {
        if (_flashlight == null || _spriteRenderer == null) return;

        if (_flashlight.isFlashlightOn)
        {
            float distance = Vector2.Distance(transform.position, _flashlight.flashlightObject.transform.position);
            float t = 1f - Mathf.Clamp01(distance / revealRadius);
            //Debug.Log("t is " + t);
            _spriteRenderer.color = Color.Lerp(darkColor, brightColor, t);
            //Debug.Log("color is " + _spriteRenderer.color);

        }
        else { _spriteRenderer.color = darkColor; }
    }

    private void OnMouseDown()
    {
        if (DialogManager.Instance != null && DialogManager.Instance.is_dialog_active_) return;

        if(_flashlight == null || !_flashlight.isFlashlightUnlocked)
        {
            if(noFlashlightSO != null) DialogManager.Instance.StartDialog(noFlashlightSO);
            return;
        }

        if (!_flashlight.isFlashlightOn)
        {
            if (darkFailureSO != null) DialogManager.Instance.StartDialog(darkFailureSO);
            return;
        }

        if (lightSuccessSO != null) DialogManager.Instance.StartDialog(lightSuccessSO);

        CollectItem();
    }

    private void CollectItem()
    {
        if(_itemData == null ) return;

        InventoryManager invManager = Object.FindAnyObjectByType<InventoryManager>();

        if (invManager != null)
        {
            invManager.AddItem(
                _itemData.ItemName,
                _itemData.Quantity,
                _itemData.Sprite,
                _itemData.ItemDescription
            );

            InventoryManager.MarkItemAsCollected(_itemData.ItemName );
        }

        Destroy(gameObject);

    }
}
