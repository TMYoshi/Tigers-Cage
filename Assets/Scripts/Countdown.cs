using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public static Countdown Instance;
    [SerializeField] private TextMeshProUGUI timer_text_;
    [SerializeField] private float remaining_time_;
    public float GetRemTime() { return remaining_time_; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            
        }
    }
    
    public void TickDown()
    {
        if(remaining_time_ > 0)
        {
            remaining_time_ -= Time.deltaTime;
        }
        else if (remaining_time_ < 0)
        {
            remaining_time_ = 0;
            timer_text_.color = Color.red;
        }

        int minutes      = Mathf.FloorToInt(remaining_time_ / 60);
        int seconds      = Mathf.FloorToInt(remaining_time_ % 60);
        int centiseconds = Mathf.FloorToInt((remaining_time_ * 100) % 100);
        timer_text_.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, centiseconds);

    }

    private void Update()
    {
        TickDown();
    }
}
