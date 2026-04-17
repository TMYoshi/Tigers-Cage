using UnityEngine;

public class PipeRotation : MonoBehaviour
{
    private bool isSubscribed = false;
    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = transform.rotation;

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

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
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
            targetRotation *= Quaternion.Euler(0, 0, -90f);
        }
    }
}
