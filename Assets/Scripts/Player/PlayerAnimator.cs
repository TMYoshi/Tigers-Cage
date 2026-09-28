using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    public static bool HasBunny = false;
    public static PlayerAnimator Instance;

    Animator animator; 

    void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public static void SetBunnyTrue()
    {
        HasBunny = true;
        Instance?.animator.SetBool("HasBunny", HasBunny);
    }

    void Start()
    {
        animator.SetBool("HasBunny", HasBunny);
    }
    
    public void Walking()
    {
        animator.SetBool("IsWalking", true);
    }

    public void StopWalking()
    {
        animator.SetBool("IsWalking", false);
    }
}
