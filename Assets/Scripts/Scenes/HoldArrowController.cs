using UnityEditor;
using UnityEngine;

public class HoldArrowController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Cutscene Settings")]
    public Camera mainCamera;// refrence to camera
    public float scrollspeed = 5f; //how fast the camera moves
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
        //limits the cameras position to stay within defined limits
        //need to change it later when hallway is created
        CamPos.x = Mathf.Clamp(CamPos.x, leftlimit, rightlimit);
        
        mainCamera.transform.position = CamPos;

        //Mouse intput detection
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

    //Checks if mouse is clicking on the arrowbutton and updates movement flag to true unitl stop clicking
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
