using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    public static PlayerSetting Instance;

    [Range(0, 1)]
    public float MasterVolume = 0.5f;
    public const string MasterVolumeKey = "MasterVolume";

    [Range(0, 1)]
    public float SFXVolume = 0.5f;
    public const string SFXVolumeKey = "SFXVolume";

    [Range(0, 1)]
    public float MusicVolume = 0.5f;
    public const string MusicVolumeKey = "MusicVolume";

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayer();
    }

    public void LoadPlayer()
    {
        MasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 0.5f);
        SFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 0.5f);
        MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f);
    }

    public void SavePlayer()
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, MasterVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, SFXVolume);
        PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume);

        PlayerPrefs.Save();
    }
}
