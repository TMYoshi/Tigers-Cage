using UnityEngine;
using UnityEngine.UI;

// Slider class that communicates with manager
public class BrightnessSlider : MonoBehaviour
{
    public Slider slider_;
    private BrightnessManager brightness_manager_;

    private void Awake()
    {
        brightness_manager_ = FindAnyObjectByType<BrightnessManager>();
        if(brightness_manager_ == null) { Debug.LogError("Brightness Manager not available"); }
    }
    
    public void SliderBrightness()
    {
        float value = slider_.value;
        brightness_manager_.SetBrightnessValue(value);
    }
}
