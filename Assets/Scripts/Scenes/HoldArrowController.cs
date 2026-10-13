using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldArrowController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Cutscene Settings")]
    public Camera mainCamera;// refrence to camera
    public float scrollspeed = 5f; //how fast the camera moves
    public float leftlimit = -1.0f;
    public float rightlimit = 50f;
    [SerializeField] Transform playerCharacter;


    public void Update()
    {
        //Move the camera only if the button is being held
        Vector3 CamPos = mainCamera.transform.position;

        CamPos.x = playerCharacter.position.x;
        //limits the cameras position to stay within defined limits
        //need to change it later when hallway is created
        CamPos.x = Mathf.Clamp(CamPos.x, leftlimit, rightlimit);
        
        mainCamera.transform.position = CamPos;
    }
}
