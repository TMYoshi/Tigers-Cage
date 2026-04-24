using UnityEngine;

public class DoorExitManager : MonoBehaviour
{
    void Start()
    {
        if (CutsceneManager.musicBoxCutsceneCompleted)
        {
            GameObject items = GameObject.Find("Items");

            if (items != null)
            {
                Transform exitDoor = items.transform.Find("OpenDoor");
                Transform oldDoor = items.transform.Find("Door");

                if (exitDoor != null) exitDoor.gameObject.SetActive(true);
                if (oldDoor != null) oldDoor.gameObject.SetActive(false);
            }
        }
    }
}
