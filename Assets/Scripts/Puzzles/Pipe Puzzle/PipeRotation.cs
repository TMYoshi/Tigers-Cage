using UnityEngine;

public class PipeRotation : MonoBehaviour
{
    private bool isSubscribed = false;

    private void Start()
    {
        if(PlayerInput.Instance != null)
        {
            PlayerInput.Instance.MouseOnClickInput += RotatePipe;
            isSubscribed = true;
        }
        else
        {
            Debug.LogError("PipeRotation has missing PlayerInput instance.");
        }
    }

    private void OnDisable()
    {
        if(isSubscribed && PlayerInput.Instance != null)
        {
            PlayerInput.Instance.MouseOnClickInput -= RotatePipe;
        }
    }

    void RotatePipe()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(PlayerInput.Instance.MouseInput);
        
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);

        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            gameObject.transform.Rotate(0, 0, -90f);
        }
    }
}
