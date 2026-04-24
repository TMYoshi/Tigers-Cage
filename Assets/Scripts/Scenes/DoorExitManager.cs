using UnityEngine;

public class DoorExitManager : MonoBehaviour
{
    [SerializeField] private GameObject brokenBox;
    [SerializeField] private GameObject batteries;

    void Start()
    {
        if (CutsceneManager.musicBoxCutsceneCompleted)
        {
            if (brokenBox != null) brokenBox.SetActive(true);
            if (batteries != null) batteries.SetActive(true);
        }
    }
}
