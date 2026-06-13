using UnityEngine;

public class IndiscriminateDialog : MonoBehaviour
{
    public static IndiscriminateDialog Instance;
    public Dialog assoc_dialog_box_;
    [SerializeField] private DialogSO dialog_;

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
            return;
        }
    }

    private void Start()
    {
        if (assoc_dialog_box_ == null)
        {
            assoc_dialog_box_ = FindAnyObjectByType<Dialog>();
        }

        DialogManager.Instance.StartDialog(dialog_);
    }

    private void Update()
    {
        if (PlayerInput.Instance.MouseClickInput) // Left mouse button
        {
            bool dialog_finished = assoc_dialog_box_.WriteDialog();

            if(dialog_finished)
            {
                Decomission();
            }
        }
    }

    private void Decomission()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
