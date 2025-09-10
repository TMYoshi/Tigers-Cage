using System.Collections.Generic;
using UnityEngine;
public class PlayerStateManager : MonoBehaviour
{
    /*
        PLAYER CONTEXT
    */
    public GameObject _InventoryMenu;
    public InventoryManager _InventoryManager;
    public enum State
    {
        Idle,
        Moving,
        Inventory
    }
    PlayerBaseState _currentState;
    public Dictionary<State, PlayerBaseState> _State = new Dictionary<State, PlayerBaseState>();

    void Start()
    {
        _State[State.Moving] = new PlayerMovingState(this);
        _State[State.Inventory] = new PlayerInvState(this);
        _State[State.Idle] = new PlayerIdleState(this);

        UpdateCurrentState(State.Idle);
    }
    public void UpdateCurrentState(State state)
    {
        _currentState = _State[state];
        _currentState.EnterState();
    }

    void Update()
    {
        //this is what runs in the update for each state command
        //there should be nothing in here other than running the scripts
        //please don't put any other code in this update function!!!
        _currentState.UpdateState();
    }
}
