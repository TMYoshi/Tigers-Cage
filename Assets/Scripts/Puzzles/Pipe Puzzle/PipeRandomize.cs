using UnityEngine;

public class PipeRandomize : MonoBehaviour
{
    private bool isSubscribed = false;
    [SerializeField] private InitializePipes pipeInitializer;

    private void Start()
    {
        if (PlayerInput.Instance != null)
        {
            PlayerInput.Instance.MouseOnClickInput += CheckAndRandomize;
            isSubscribed = true;
        }
        else
        {
            Debug.LogError("PipeRotation has missing PlayerInput instance.");
        }
    }


    private void OnDisable()
    {
        if (isSubscribed && PlayerInput.Instance != null)
        {
            PlayerInput.Instance.MouseOnClickInput -= CheckAndRandomize;
        }
    }

    private void CheckAndRandomize()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(PlayerInput.Instance.MouseInput);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);

        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            if (pipeInitializer != null)
            {
                pipeInitializer.RandomizePipes();
            }
        }
    }
}
