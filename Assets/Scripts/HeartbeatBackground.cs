using UnityEngine;

public class HeartbeatBackground : MonoBehaviour
{
    public static bool Stressed {get; private set;} = true;
    [SerializeField] GameObject heartbeatBackground;

    public void TurnStressDown()
    {
        Stressed = false;
        heartbeatBackground.SetActive(false);
    }

    void Start()
    {
        if(!Stressed) 
            heartbeatBackground.SetActive(false);
        else
            heartbeatBackground.SetActive(true);
    }
}
