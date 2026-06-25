using UnityEngine;

public class IndiscriminateDialog : MonoBehaviour
{
    public static IndiscriminateDialog Instance;
    public DialogManager assoc_dialog_box_;
    [SerializeField] private DialogSO dialog_;
    public static bool is_active_ = false;
    private bool start_dialog_ = true;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {   
        if(is_active_)
        {
            if (assoc_dialog_box_ == null && start_dialog_ == true)
            {
                assoc_dialog_box_ = GetComponent<DialogManager>();
                DialogManager.Instance.StartDialog(dialog_);
                start_dialog_ = false;
                return;
            }

            if (PlayerInput.Instance.MouseClickInput) // Left mouse button
            {
                if(!DialogManager.Instance.AdvanceDialog())
                {
                    is_active_ = false;
                    return;
                }
            }
        }
    }
}
