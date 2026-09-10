using UnityEngine;
using UnityEngine.Events;

public class HeartbeatBackground : MonoBehaviour
{
    public static bool Stressed {get; private set;} = true;
    public static HeartbeatBackground Instance;
    public UnityEvent OnStressed;
    public UnityEvent OnStressedDown;
    [SerializeField] GameObject heartbeatBackground;

    void Awake()
    {
        Instance = this;
    }

    public static void TurnStressDown()
    {
        Stressed = false;
        Instance.heartbeatBackground.SetActive(false);
        Instance.OnStressedDown.Invoke();
    }
    
    public static void TurnStressUp()
    {
        Stressed = true;
        Instance.heartbeatBackground.SetActive(true);
        Instance.OnStressed.Invoke();
    }

    void Start()
    {
        if(Stressed) TurnStressUp();
        if(!Stressed) TurnStressDown();
    }
}
