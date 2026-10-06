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

    public void UpdateSaveMaster(float _newValue)
    {
        PlayerSetting.Instance.MasterVolume = _newValue;
        PlayerSetting.Instance.SavePlayer();
    }

    public void UpdateSaveSFX(float _newValue)
    {
        PlayerSetting.Instance.SFXVolume = _newValue;
        PlayerSetting.Instance.SavePlayer();
    }

    public void UpdateSaveMusic(float _newValue)
    {
        PlayerSetting.Instance.MusicVolume = _newValue;
        PlayerSetting.Instance.SavePlayer();
    }

    public void UpdateSaveBrightness(float _newValue)
    {
        PlayerSetting.Instance.Brightness = _newValue;
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
