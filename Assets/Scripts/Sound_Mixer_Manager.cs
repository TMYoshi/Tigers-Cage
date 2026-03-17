using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Sound_Mixer_Manager : MonoBehaviour
{
    [SerializeField] private AudioMixer audio_mixer_;
    [Tooltip("All these attributes are manually derived from the Pause UI Settings tab")]
    [SerializeField] private Slider master_slider_;
    [SerializeField] private Slider sfx_slider_;
    [SerializeField] private Slider music_slider_;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Master Volume") || PlayerPrefs.HasKey("SFX Volume") || PlayerPrefs.HasKey("Music Volume"))
        {
            Debug.Log("Setting volume");
            LoadVolume();
        }
    }

    public void SetMasterVolume()
    {
        float level = master_slider_.value;
        PlayerPrefs.SetFloat("Master Volume", level);
        audio_mixer_.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetSFXVolume()
    {
        float level = sfx_slider_.value;
        audio_mixer_.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("SFX Volume", level);

    }
    
    public void SetMusicVolume()
    {
        float level = music_slider_.value;
        audio_mixer_.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("Music Volume", level);
    }
    private void LoadVolume()
    {
        master_slider_.value = PlayerPrefs.GetFloat("Master Volume");
        sfx_slider_.value = PlayerPrefs.GetFloat("SFX Volume");
        music_slider_.value = PlayerPrefs.GetFloat("Music Volume");

        SetMasterVolume();
        SetSFXVolume();
        SetMusicVolume();
    }
}
