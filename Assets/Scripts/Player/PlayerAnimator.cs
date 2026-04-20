using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    Animator animator; 

    void Awake()
    {
        animator = GetComponent<Animator>();
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
