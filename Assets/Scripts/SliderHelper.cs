using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SliderHelper : MonoBehaviour
{
    [SerializeField] private Slider master_slider_;
    [SerializeField] private Slider sfx_slider_;
    [SerializeField] private Slider music_slider_;
    [SerializeField] private Slider brightness_slider_;

    void Start()
    {
        UpdateSlider();
    }

    public void UpdateSave()
    {
        PlayerSetting.Instance.MasterVolume = master_slider_.value;
        PlayerSetting.Instance.SFXVolume = sfx_slider_.value;
        PlayerSetting.Instance.MusicVolume = music_slider_.value;
        PlayerSetting.Instance.Brightness = brightness_slider_.value;
        PlayerSetting.Instance.SavePlayer();
    }

    private void UpdateSlider()
    {
        master_slider_.value = PlayerSetting.Instance.MasterVolume;
        sfx_slider_.value = PlayerSetting.Instance.SFXVolume;
        music_slider_.value = PlayerSetting.Instance.MusicVolume;
        brightness_slider_.value = PlayerSetting.Instance.Brightness;
    }
}
