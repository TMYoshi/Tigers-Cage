using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight Settings")]
    [SerializeField] private GameObject flashlightObject;
    [SerializeField] private Light2D flashlight; // ref to Light2D component
    [SerializeField] private KeyCode toggleKey = KeyCode.F;

    [Header("Follow Settings")]
    [SerializeField] private float followSpeed = 10f;

    // since it doesnt take inventory
    private const string FLASHLIGHT_UNLOCKED_KEY = "FlashlightUnlocked";
    private bool isFlashlightUnlocked = false;

    private Camera mainCamera;
    private bool isFlashlightOn = false;
    private Transform playerTransform;

    private void Start()
    {
        mainCamera = Camera.main;
        playerTransform = transform;

        isFlashlightUnlocked = PlayerPrefs.GetInt(FLASHLIGHT_UNLOCKED_KEY, 0) == 1;

        if (flashlightObject != null)
        {
            flashlightObject.SetActive(false);
        }
        DontDestroyOnLoad(flashlightObject);
    }

    private void Update()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                return;
            }
        }

        if (isFlashlightUnlocked && Input.GetKeyDown(toggleKey))
        {
            ToggleFlashlight();
        }

        if(isFlashlightOn && flashlightObject != null){
            UpdateFlashlightPosition();
        }

        // DEBUG: R to reset flashlight unlock status
        #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetFlashlightUnlock();
                Debug.Log("Flashlight unlock reset!");
            }
        #endif
    }

    void ToggleFlashlight()
    {
        Debug.Log("flashlight toggled");
        isFlashlightOn = !isFlashlightOn;
        if (flashlightObject != null)
        {
            flashlightObject.SetActive(isFlashlightOn);
        }
    }

    void UpdateFlashlightPosition()
    {
        if (mainCamera == null)
        {
            return;
        }

        // get mouse position in world
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;


        // smoothly move flashlight
        flashlightObject.transform.position = Vector3.Lerp(
            flashlightObject.transform.position,
            mousePos,
            followSpeed * Time.deltaTime 
        );
    }

    // toggle for other scripts
    public void SetFlashlightState(bool state)
    {
        isFlashlightOn = state;
        if(flashlightObject != null)
        {
            flashlightObject.SetActive(isFlashlightOn);
        }
    }

    // unsure how we were saving data, ill have to ask later
    public void UnlockFlashlight()
    {
        isFlashlightUnlocked = true;
        PlayerPrefs.SetInt(FLASHLIGHT_UNLOCKED_KEY, 1);
        PlayerPrefs.Save();
        Debug.Log("flashlight unlocked");
    }

    public void ResetFlashlightUnlock()
    {
        isFlashlightUnlocked = false;
        PlayerPrefs.SetInt(FLASHLIGHT_UNLOCKED_KEY, 0);
        PlayerPrefs.Save();
    }
}
