using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] private bool is_valid_spot_;
    private PlayerStateManager player_;
    public bool IsValidSpot() { return is_valid_spot_; }

    private void Awake()
    {
        player_ = PlayerStateManager.Instance;
    }
    
    private void Start()
    {
        StartCoroutine(Startup());
    }
    IEnumerator Startup()
    {
        yield return new WaitUntil(() => player_._State.ContainsKey(PlayerStateManager.State.Hiding));
        player_.UpdateCurrentState(PlayerStateManager.State.Hiding);
    }
}
