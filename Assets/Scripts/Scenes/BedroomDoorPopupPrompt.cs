using UnityEngine;
using UnityEngine.Playables;

public class BedroomDoorPopupPrompt : MonoBehaviour
{
    [SerializeField] private PopupManager Popup;
    [SerializeField] private BoxCollider2D DoorCollider;
    [SerializeField] private DialogSO MissingCameraDialog;
    [SerializeField] private string requiredItemKey = "WorkingCamera";

    private bool isPopupActive = false;

    void OnEnable()
    {
        if (PlayerInput.Instance != null)
        {
            PlayerInput.Instance.MouseOnClickInput += OnClick;
        }
    }

    void OnDisable()
    {
        if (PlayerInput.Instance != null)
        {
            PlayerInput.Instance.MouseOnClickInput -= OnClick;
        }
    }

    void OnClick()
    {
        if (isPopupActive || DialogManager.Instance.is_dialog_active_) return;

        Vector2 mousePos = PlayerInput.Instance.MouseInput;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Raycast hit nothing!");
        }

        if (hit.collider != DoorCollider) return;

        if (InventoryManager.IsItemCollected(requiredItemKey))
        {
            Popup.SetCurrPopup(Popup.door_exit_popup_);
            Popup.SetUIPopuopOn();

            isPopupActive = true;
        }
        else
        {
            Debug.Log("Failure!");
            DialogManager.Instance.StartDialog(MissingCameraDialog);
        }
    }
}
