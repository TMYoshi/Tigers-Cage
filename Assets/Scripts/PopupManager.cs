using UnityEngine;
using System;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;
    [SerializeField] TextMeshProUGUI text;
    public GameObject controls_popup_;
    public GameObject hb_minigame_popup_;
    public GameObject door_exit_popup_;
    private GameObject curr_popup_;
    public void SetCurrPopup(GameObject popup) { curr_popup_ = popup; }
    Animator animator;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;

        animator = GetComponent<Animator>();
    }

    // Prompt when first entering game
    void Start()
    {
        //curr_popup_ = controls_popup_;
        //SetUIPopuopOn();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void SetPopupOnMessage(string _message, ref Action _inputAction)
    {
        text.text = _message;
        animator.SetBool("Show", true);
        _inputAction -= SetpopupOff;
        _inputAction += SetpopupOff;
    }

    public void SetpopupOff()
    {
        animator.SetBool("Show", false);
        PlayerInput.Instance.InvOnClick -= SetpopupOff;
        PlayerInput.Instance.FlashInput -= SetpopupOff;
    }

    public void SetUIPopuopOn()
    {
        // Use cases: Controls, Heartbeat Tutorial, Door Exit
        /*
        curr_popup_.gameObject.SetActive(true);
        animator.SetBool("Show UI", true);
        PlayerStateManager.Instance.UpdateToNullState();
        */
    }

    public void SetUIPopupOff()
    {
        animator.SetBool("Show UI", false);
        StartCoroutine(WaitForPopupAnimation());
    }

    #region WaitForPopupAnimation
    private IEnumerator WaitForPopupAnimation()
    {
       AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
       
       // Keep waiting until the "Exit" animation is actually playing
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("ShowToIdleUI"))
        {
            yield return null;
        }   

        // Now wait for the "Exit" animation to complete
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0f)
        {
            yield return null;
        }

        curr_popup_.gameObject.SetActive(false);
        PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Idle);
    }
    #endregion
}
