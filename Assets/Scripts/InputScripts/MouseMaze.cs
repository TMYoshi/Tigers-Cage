using UnityEngine;

public class MouseMaze : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = false;
    }
    [SerializeField] float speed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = PlayerInput.Instance.MouseMovement;

        transform.position += new Vector3(delta.x, delta.y, 0f) * speed;
    }
}
