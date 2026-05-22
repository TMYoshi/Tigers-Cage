using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;
    [SerializeField] TextMeshProUGUI text;
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

    public void SetpopupOnJournal()
    {
        text.text = "Press E to use Journal";
        animator.SetBool("Show", true);
        PlayerInput.Instance.InvOnClick += SetpopupOff;
    }

    public void SetpopupOff()
    {
        animator.SetBool("Show", false);
        PlayerInput.Instance.InvOnClick -= SetpopupOff;
    }
}
