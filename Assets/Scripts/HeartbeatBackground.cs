using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class HeartbeatBackground : MonoBehaviour
{
    public static bool Stressed {get; private set;} = true;
    public static HeartbeatBackground Instance;
    public UnityEvent OnStressed;
    public UnityEvent OnStressedDown;
    [SerializeField] GameObject heartbeatBackground;
    Animator veinAnimator;

    void Awake()
    {
        Instance = this;
        veinAnimator = GetComponent<Animator>();
    }

    public void SetShowBool(bool _show)
    {
        veinAnimator.SetBool("Show", _show);
    }

    public static void TurnStressDown()
    {
        Stressed = false;
        Instance.OnStressedDown.Invoke();
    }
    
    public static void TurnStressUp()
    {
        Stressed = true;
        Instance.OnStressed.Invoke();
    }
}
