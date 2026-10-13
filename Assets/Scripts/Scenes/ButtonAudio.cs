using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    [SerializeField] AudioClip currentClip;

    public void PlaySFXBaseOnClip()
    {
        SFXManager.Instance.PlaySFXClip(currentClip);
    }

    public void PlaySFX(AudioClip _clip)
    {
        SFXManager.Instance.PlaySFXClip(_clip);
    }

    public void PlaySFXLoop(AudioClip _clip)
    {
        SFXManager.Instance.PlaySFXClipLoop(_clip);
    }
}
