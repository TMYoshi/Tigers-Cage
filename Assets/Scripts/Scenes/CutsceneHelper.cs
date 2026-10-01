using UnityEngine;

//runs some cutsceneEvents
public class CutsceneHelper : MonoBehaviour
{
    public void StartCountdown()
    {
        if (Countdown.Instance != null)
        {
            Countdown.Instance.gameObject.SetActive(true);
            Countdown.is_active_ = true;
            Debug.Log("Music Box Cutscene finished, countdown: " + Countdown.is_active_);

            IndiscriminateDialog.Instance.gameObject.SetActive(true);
            IndiscriminateDialog.is_active_ = true;
        }
    }
}
