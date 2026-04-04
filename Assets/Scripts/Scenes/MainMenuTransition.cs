using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MainMenuTransition : MonoBehaviour
{
    public static MainMenuTransition Instance;
    Animator animator;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        animator = GetComponent<Animator>();
    }

    public float ChangeSceneWithTigerAnimation()
    {
        animator.SetTrigger("Tiger");

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "BeginTransition")
            {
                return clip.length;
            }
        }

        return 0f;
    }
}
