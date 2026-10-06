using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    public static PlayerSetting Instance;

    [Range(0, 1)]
    public float MasterVolume;
    private const string MasterVolumeKey = "Master Volume";

    [Range(0, 1)]
    public float SFXVolume;
    private const string SFXVolumeKey = "SFX Volume";

    [Range(0, 1)]
    public float Brightness;
    private const string BrightnessKey = "Brightness";

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
        if(PlayerPrefs.HasKey(MasterVolumeKey))
            MasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 0.5f);

        if(PlayerPrefs.HasKey(SFXVolumeKey))
            SFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 0.5f);

        if(PlayerPrefs.HasKey(BrightnessKey))
            Brightness = PlayerPrefs.GetFloat(BrightnessKey, 0.5f);
    }
}
