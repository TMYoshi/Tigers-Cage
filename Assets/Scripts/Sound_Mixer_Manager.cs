using UnityEngine;
using UnityEngine.Audio;

public class Sound_Mixer_Manager : MonoBehaviour
{
    [SerializeField] private AudioMixer audio_mixer_;

    public void SetMasterVolume(float level)
    {
        audio_mixer_.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetSFXVolume(float level)
    {
        audio_mixer_.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);

    }
    
    public void SetMusicVolume(float level)
    {
        audio_mixer_.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
        
    }
}
