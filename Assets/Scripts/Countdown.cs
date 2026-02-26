using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Countdown : MonoBehaviour
{
    public Countdown Instance;
    private static bool is_active_ = false;
    [SerializeField] private TextMeshProUGUI timer_text_;
    [SerializeField] private float remaining_time_;
    private PlayerStateManager player_;
    public float GetRemTime() { return remaining_time_; }

    // TODO: Make Singleton

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            
        }
        // To activate countdown, affect the player pref?
        if (is_active_)
        {
            remaining_time_ = PlayerPrefs.GetFloat("countdown_value");
        }
        else if (PlayerPrefs.GetFloat("countdown_value") <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            is_active_ = true;
        }
    }
    
    private void TickDown()
    {
        if(remaining_time_ > 0)
        {
            remaining_time_ -= Time.deltaTime;
        }
        else if (remaining_time_ < 0)
        {
            is_active_ = false;
            remaining_time_ = 0;
            timer_text_.color = Color.red;

            PlayerPrefs.DeleteKey("countdown_value");

            player_ = PlayerStateManager.Instance;
            if(player_.GetCurrentState() is not PlayerHidingState)
            {
                SceneController.scene_controller_instance.FadeAndLoadScene("GameOver");
            }
        }

        int minutes      = Mathf.FloorToInt(remaining_time_ / 60);
        int seconds      = Mathf.FloorToInt(remaining_time_ % 60);
        int centiseconds = Mathf.FloorToInt(remaining_time_ * 100 % 100);
        timer_text_.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, centiseconds);

    }

    private void Update()
    {
        TickDown();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("countdown_value", remaining_time_);
    }
}
