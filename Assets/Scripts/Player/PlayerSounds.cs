using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip[] footstepSounds;

    public void PlayFootstepRandom()
    {
        SFXManager.Instance.PlaySFXClip
            (footstepSounds[Random.Range(0, footstepSounds.Length)]);
    }
}
