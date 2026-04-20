using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    [SerializeField] private AudioSource sfx_obj_;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFXClip(AudioClip audio_clip, Transform spawn_transform = null, float volume = 1)
    {
        if(spawn_transform == null) spawn_transform = this.transform;

        // Spawn in a game object
        if (audio_clip == null || spawn_transform == null) return;
        AudioSource audio_source = Instantiate(sfx_obj_, spawn_transform.position, Quaternion.identity);

        // Assign audio source
        audio_source.clip = audio_clip;

        //varying pitch and volume to sound a lil better
        audio_source.pitch = Random.Range(0.8f, 1.2f);
        audio_source.volume = volume * Random.Range(0.8f, 1.2f);

        // Play sound
        audio_source.Play();

        // Find length of audio clip
        float audio_len = audio_source.clip.length;

        // Destroy once audio finishes
        Destroy(audio_source.gameObject, audio_len);
        return;
    }
}
