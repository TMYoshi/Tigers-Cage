using System.Collections.Generic;
using UnityEngine;
public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;
    /*
        PLAYER CONTEXT
    */
    public GameObject _InventoryMenu;
    public InventoryManager _InventoryManager ;
    public ItemManager _ItemManager;
    //dunno if I should make proper getters and setters for this but I think it should be fine for now

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum State
    {
        Idle,
        Moving,
        Inventory,
        DialogItem,
        SpecialItem
    }
    PlayerBaseState _currentState;
    public Dictionary<State, PlayerBaseState> _State = new Dictionary<State, PlayerBaseState>();

    void Start()
    {
        _State[State.Moving] = new PlayerMovingState(this);
        _State[State.Inventory] = new PlayerInvState(this);
        _State[State.DialogItem] = new PlayerDialogItemState(this);
        _State[State.SpecialItem] = new PlayerSpecialItemState(this);
        _State[State.Idle] = new PlayerIdleState(this);

        UpdateCurrentState(State.Idle); // immediately start as idle when first instantiated
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

