using UnityEditor;
using UnityEngine;

public class HoldArrowController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Cutscene Settings")]
    public Camera mainCamera;
    public float scrollspeed = 5f;
    public float leftlimit = -1.0f;
    public float rightlimit = 50f;

    //Keep track on click hold
    private bool moveLeft = false;
    private bool moveRight = false;


    public void Update()
    {
        //Move the camera only if the button is being held
        Vector3 CamPos = mainCamera.transform.position;

        if (moveLeft)
        {
            CamPos.x -= scrollspeed * Time.deltaTime;
        }

        if (moveRight)
        {
            CamPos.x += scrollspeed * Time.deltaTime;
        }

        CamPos.x = Mathf.Clamp(CamPos.x, leftlimit, rightlimit);

        mainCamera.transform.position = CamPos;

        //Mouse intput
        if (Input.GetMouseButtonDown(0))
        {
            CheckArrowHold(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            moveLeft = false;
            moveRight = false;
        }
    }

    void CheckArrowHold(bool holding)
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.zero);

        if (hit.collider != null) {


            if (hit.collider.name == "LeftArrow")
            {
                moveLeft = holding;
            }
            
            else if (hit.collider.name == "RightArrow")
            {
                moveRight = holding;
            }
        }
    }


}
