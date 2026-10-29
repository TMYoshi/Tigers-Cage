using UnityEngine;

public class ButtonUtility : MonoBehaviour
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

    public void SetCursorHover()
    {
        if(CursorManager.Instance != null)
            CursorManager.Instance.UpdateToState(CursorManager.CursorState.Hover);
    }

    public void SetCursorIdle()
    {
        if(CursorManager.Instance != null)
            CursorManager.Instance.UpdateToState(CursorManager.CursorState.Idle);
    }
}
