using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SliderHelper : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMix;

    [SerializeField] private Slider master_slider_;
    [SerializeField] private Slider sfx_slider_;
    [SerializeField] private Slider music_slider_;

    void Start()
    {
        UpdateSlider();
    }

    public void UpdateSaveMaster(float _newValue)
    {
        PlayerSetting.Instance.MasterVolume = _newValue;
        PlayerSetting.Instance.SavePlayer();

        float masterVolume = Mathf.Log10(Mathf.Clamp(PlayerSetting.Instance.MasterVolume, 0.0001f, 1f)) * 20;
        masterMix.SetFloat("MasterVolume", masterVolume);
    }

    public void UpdateSaveSFX(float _newValue)
    {
        PlayerSetting.Instance.SFXVolume = _newValue;
        PlayerSetting.Instance.SavePlayer();

        float sfxVolume = Mathf.Log10(Mathf.Clamp(PlayerSetting.Instance.SFXVolume, 0.0001f, 1f)) * 20;
        masterMix.SetFloat("SFXVolume", sfxVolume);
    }

    public void UpdateSaveMusic(float _newValue)
    {
        PlayerSetting.Instance.MusicVolume = _newValue;
        PlayerSetting.Instance.SavePlayer();

        float musicVolume = Mathf.Log10(Mathf.Clamp(PlayerSetting.Instance.MusicVolume, 0.0001f, 1f)) * 20;
        masterMix.SetFloat("MusicVolume", musicVolume);
    }

    private void UpdateSlider()
    {
        master_slider_.value = PlayerSetting.Instance.MasterVolume;
        sfx_slider_.value = PlayerSetting.Instance.SFXVolume;
        music_slider_.value = PlayerSetting.Instance.MusicVolume;

        float masterVolume = Mathf.Log10(Mathf.Clamp(PlayerSetting.Instance.MasterVolume, 0.0001f, 1f)) * 20;
        masterMix.SetFloat("MasterVolume", masterVolume);

        float sfxVolume = Mathf.Log10(Mathf.Clamp(PlayerSetting.Instance.SFXVolume, 0.0001f, 1f)) * 20;
        masterMix.SetFloat("SFXVolume", sfxVolume);

        float musicVolume = Mathf.Log10(Mathf.Clamp(PlayerSetting.Instance.MusicVolume, 0.0001f, 1f)) * 20;
        masterMix.SetFloat("MusicVolume", musicVolume);
    }
}
