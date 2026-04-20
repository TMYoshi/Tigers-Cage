using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public void PlaySFX(AudioClip _clip)
    {
        SFXManager.Instance.PlaySFXClip(_clip);
    }
}
