using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI text_component_;
    public string text_name_ = "Test"; // TODO: Later make this its own component in the prefab
    //public List<string> lines_;
    public Image sprite_;

    [SerializeField]
    private DialogSO current_dialog_;
    private int index_ = 0;
    public bool is_dialog_active_; // Used for opener animation
    public bool is_dialog_ending_; // Used for checking if dialog still available
    public bool dialog_opener_ = true;

    [SerializeField]
    private float text_speed_ = 0.4f;
    public Animator dialog_animator_;

    #region Awake
    // Implement Singleton so that SOs can access Dialog
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    #region TypeLine
    // Good guide for IEnumerator: https://www.reddit.com/r/Unity3D/comments/yc8fi1/question_what_does_ienumerator_do_i_just_cant/
    // TLDR: IEnumerator ~= foreach object. Has methods: gets current + moves next
    public IEnumerator TypeLine()
    {
        Debug.Log("Current line: " + index_);
        DialogLine line = current_dialog_.lines_[index_];

        sprite_ = line.speaker_.sprite_;
        text_name_ = line.speaker_.actor_name_;

        // Set full text up front
        string fullText = text_name_ + "\n" + line.text_;
        text_component_.text = fullText;

        // Start revealing *after* the name + newline
        text_component_.maxVisibleCharacters = text_name_.Length + 1;

        // Reveal remaining characters
        for (int i = text_component_.maxVisibleCharacters; i <= fullText.Length; i++)
        {
            text_component_.maxVisibleCharacters = i;
            yield return new WaitForSeconds(text_speed_);
        }
    }
    #endregion


    #region StartDialog
    public void StartDialog(DialogSO dialog_so)
    {
        current_dialog_ = dialog_so;
        index_ = 0;

        gameObject.SetActive(true);
        dialog_animator_.SetTrigger("Enter");
        dialog_opener_ = false;

        StartCoroutine(TypeLine());
        is_dialog_active_ = true;
    }
    #endregion

    #region AdvanceDialog
    public bool AdvanceDialog()
    {
        if (is_dialog_ending_)
        {
            return true;
        }

        dialog_animator_.SetTrigger("Bob Sprite");

        if (index_ < current_dialog_.lines_.Length - 1)
        {
            ++index_;

            StartCoroutine(TypeLine());

            return false;
        }
        else
        {
            EndDialog();
            gameObject.SetActive(false);

            return true;
        }
    }
    #endregion

    #region EndDialog
    public void EndDialog()
    {
        index_ = 0;
        is_dialog_active_ = false;

        dialog_animator_.SetTrigger("Exit"); // TODO: Fix so actually triggers animation lol
    }
    #endregion

    #region EndLine
    public void EndLine()
    {
        StopAllCoroutines();
        text_component_.maxVisibleCharacters = text_component_.textInfo.characterCount;
    }
    #endregion

    #region Setters + Getters
    public int GetIndex()
    {
        return index_;
    }

    public DialogSO GetCurrentDialogSO()
    {
        return current_dialog_;
    }
    #endregion
    /*
    
    [Header("UI References")]
    public TextMeshProUGUI text_component_;
    public List<string> lines_;
    public string text_name_ = "Test";
    public Animator dialog_animator_;
    public Sprite sprite_;

    private bool dialog_opener_ = true;
    private int index_ = 0;
    private bool is_dialog_ending_ = false;

    private DialogSO current_dialog_;

    #region old stuff for ref
    // Basically Update
    public bool PlayDialog()
    {
        // prevent input during dialog ending
        if (is_dialog_ending_)
        {
            return false;
        }

        if (dialog_opener_)
        {
            dialog_opener_ = false;

            Dialog.Instance.StartDialogue(dialog_so);
            return false;
        }
        else if (text_component_.text == text_name_ + '\n' + lines_[index_])
        {
            //dialog_animator_.SetTrigger("Bob Sprite");
            // Proceeds to next dialog
            return NextLine();
        }
        else
        {
            // Finishes current dialog
            StopAllCoroutines();
            text_component_.text = text_name_ + '\n' + lines_[index_];
        }

        return false; // changed from true, not ending dialog here anymore just finishing current line
    }



    public void SetLines(List<string> lines)
    {
        lines_ = lines;
    }

    public List<string> GetLines()
    {
        return lines_;
    }
    #endregion
    */
}