using UnityEngine;
using UnityEngine.InputSystem;

public class MouseStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log("Screen position: " + screenPos);

        //Mouse position in screen coordinates
        Mouse.current.WarpCursorPosition(screenPos);
    }

}
