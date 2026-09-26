using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.SetInt("FlashlightUnlocked", 0);
        PlayerPrefs.Save();
        Debug.Log("Locked flashlight");
    }

}
