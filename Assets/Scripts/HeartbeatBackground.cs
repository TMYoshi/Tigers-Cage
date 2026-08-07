using UnityEngine;
using UnityEngine.Events;

public class HeartbeatBackground : MonoBehaviour
{
    public static bool Stressed {get; private set;} = true;
    public UnityEvent OnStressed;
    public UnityEvent OnStressedDown;
    [SerializeField] GameObject heartbeatBackground;

    public void TurnStressDown()
    {
        Stressed = false;
        heartbeatBackground.SetActive(false);
        OnStressedDown.Invoke();
    }

    void Start()
    {
        if(!Stressed) 
            heartbeatBackground.SetActive(false);
        else
        {
            heartbeatBackground.SetActive(true);
            OnStressed.Invoke();
        }
    }
}
