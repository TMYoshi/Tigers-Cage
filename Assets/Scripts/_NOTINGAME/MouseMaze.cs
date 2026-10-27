using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMaze : MonoBehaviour
{

    [SerializeField] float speed = 0.003f;

    private Rigidbody2D rb;
    private bool canMove = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()
    {
        if(Camera.main != null && Mouse.current != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Debug.Log("Screen position: " + screenPos);

            //Mouse position in screen coordinates
            Mouse.current.WarpCursorPosition(screenPos);
        }
        else
        {
            Debug.LogWarning("Camera.main or Mouse.current is null. Cannot warp cursor position.");
        }

        yield return null;
        yield return null;
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;//Not ready to move yet
        }
        
        if (PlayerInput.Instance == null)
        {
            return;//No input manager, do nothing
        }

        Vector2 delta = PlayerInput.Instance.MouseMovement;
        Vector2 movement = delta * speed;

        rb.MovePosition(rb.position + movement);
    }

}
