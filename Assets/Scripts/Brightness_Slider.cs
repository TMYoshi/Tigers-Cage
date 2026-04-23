using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Brightness_Slider : MonoBehaviour
{
    public Slider slider_;
    public Volume volume_;
    private VolumeProfile volume_profile_;
    private ColorAdjustments brightness_;

    private void Awake()
    {
        volume_ = FindAnyObjectByType<Volume>();
    }

    private void Start()
    {
        volume_profile_ = volume_.profile;
        volume_profile_.TryGet<ColorAdjustments>(out brightness_);
    }

    public void SetBrightness(float value)
    {
        Gradient gradient = new Gradient();

        var colors = new GradientColorKey[3];
        colors[0] = new GradientColorKey(Color.white, 0.0f);
        colors[1] = new GradientColorKey(Color.gray4, 0.5f);
        colors[2] = new GradientColorKey(Color.gray1, 1.0f);

        gradient.SetColorKeys(colors);

        if(value >= 0f)
        {
            brightness_.colorFilter.value = gradient.Evaluate(value);
        }
        else
        {
            brightness_.colorFilter.value = gradient.Evaluate(0.5f);
        }
    }
}
