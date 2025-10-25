using System;
using System.Collections.Generic;
using UnityEngine;

// This file helps manages animations + dialog of individual entities
public class Dialog : MonoBehaviour
{
    private bool is_dialog_ending_ = false;
    public DialogSO current_convo_;
    public List<DialogSO> convos_;

    public bool WriteDialog()
    {
        // prevent input during dialog ending
        if (is_dialog_ending_)
        {
            return false;
        }

        // Kick off dialog if not active
        if (!DialogManager.Instance.is_dialog_active_)
        {
            Debug.Log("Starting");
            CheckForNewConvo();
            DialogManager.Instance.StartDialog(current_convo_);
            return false;
        }

        // Check if initiated current dialog
        if (DialogManager.Instance.text_component_.text ==
            DialogManager.Instance.text_name_ + '\n' +
            DialogManager.Instance.GetCurrentDialogSO().lines_[DialogManager.Instance.GetIndex()].text_)
        {
            Debug.Log("Advancing");
            return DialogManager.Instance.AdvanceDialog();
        }
        // Complete current dialog
        else
        {
            StopAllCoroutines();
            DialogManager.Instance.EndLine();
            // TODO: ok so like the dialog box works again but i had to sacrifice the complete current dialog thing
            // But lowkey I just want a deliverable project so we can fix this later
        }

        return false;

        // Finish current dialog
        //else
        //{
        //StopAllCoroutines();
        // TODO?: Genuinely what is this omg
        //DialogManager.Instance.text_component_.text = DialogManager.Instance.text_name_ + '\n'
        //                                          + DialogManager.Instance.GetCurrentDialogSO().lines_[DialogManager.Instance.GetIndex()].text_;
        //}
    }

    private void CheckForNewConvo()
    {
        for (int index = 0; index < convos_.Count; ++index)
        {
            DialogSO convo = convos_[index];
            if (convo != null && convo.IsConditionMet())
            {
                current_convo_ = convo;
                return;
            }
        }
        current_convo_ = convos_[convos_.Count - 1]; // Use "failed" conversation
    }

    /*
    #region NextLine
    public bool NextLine()
    {
        if (is_dialog_ending_) return true;

        dialog_animator_.SetTrigger("Bob Sprite");
        if (index_ < lines_.Count - 1)
        {
            ++index_;
            Debug.Log("Current line: " + index_);
            StartCoroutine(TypeLine());
            return false;
        }
        else
        {
            is_dialog_ending_ = true;

            Debug.Log("Finishing Dialog");
            text_component_.text = "";

            dialog_animator_.SetTrigger("Exit"); // TODO: Fix so actually triggers animation lol
            dialog_opener_ = true;

            is_dialog_ending_ = false; // reset again
            gameObject.SetActive(false); // hide dialog until next item is clicked
            return true;
        }
    }
    #endregion
    */
}
