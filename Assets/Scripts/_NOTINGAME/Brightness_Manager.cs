using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

// Class that actually sets brightness
public class BrightnessManager : MonoBehaviour
{
    public Volume volume_;
    private VolumeProfile volume_profile_;
    private ColorAdjustments brightness_;
    private Gradient gradient_ = new Gradient();

    private void Awake()
    {
        volume_ = FindAnyObjectByType<Volume>();

        if(volume_ == null) { Debug.LogError("Global Volume not available"); }
    }

    private void Start()
    {
        var colors = new GradientColorKey[3];
        colors[0] = new GradientColorKey(Color.white, 1.0f);
        colors[1] = new GradientColorKey(Color.gray4, 0.5f);
        colors[2] = new GradientColorKey(Color.gray1, 0.0f);

        gradient_.SetColorKeys(colors);

        volume_profile_ = volume_.profile;
        volume_profile_.TryGet<ColorAdjustments>(out brightness_);

        if(PlayerPrefs.HasKey("Brightness"))
        {
            LoadBrightness();
        }
        else
        {
            SetBrightnessValue(0.5f);
        }
    }

    public void SetBrightnessValue(float value)
    {
        brightness_.colorFilter.value = gradient_.Evaluate(value);
        PlayerPrefs.SetFloat("Brightness", value);
    }

    private void LoadBrightness()
    {
        brightness_.colorFilter.value = gradient_.Evaluate(PlayerPrefs.GetFloat("Brightness"));
    }
}
