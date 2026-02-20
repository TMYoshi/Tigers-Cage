using System;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] private bool is_valid_spot_;
    private PlayerStateManager player_;
    // TODO: [SerializeField] private Cutscene failure_cutscene;

    public bool IsValidSpot() { return is_valid_spot_; }

    private void Awake()
    {
        player_ = FindAnyObjectByType<PlayerStateManager>();
    }
    
    private void Start()
    {
        player_.UpdateCurrentState(PlayerStateManager.State.Hiding);
    }
}
