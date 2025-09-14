using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI text_component_;
    public List<string> lines_; // C# does not have vectors (C++) (BOOOO) so use an array :(
    public float text_speed_ = 0.1f;
    public string text_name_ = "Test";
    public Animator dialog_animator_;

    private bool dialog_opener_ = true;
    private int index_ = 0;
    private bool is_dialog_ending_ = false;

    // Update is called once per frame
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
            StartDialogue();
            return false;
        }
        else if (text_component_.text == text_name_ + '\n' + lines_[index_])
        {
            dialog_animator_.SetTrigger("Bob Sprite");
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

    public void StartDialogue()
    {
        gameObject.SetActive(true);
        dialog_animator_.SetTrigger("Enter");
        index_ = 0;
        StartCoroutine(TypeLine());
    }

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

    public void SetLines(List<string> lines)
    {
        lines_ = lines;
    }

    public List<string> GetLines()
    {
        return lines_;
    }

    // Good guide for IEnumerator: https://www.reddit.com/r/Unity3D/comments/yc8fi1/question_what_does_ienumerator_do_i_just_cant/
    // TLDR: IEnumerator ~= foreach object. Has methods: gets current + moves next
    public IEnumerator TypeLine()
    {
        text_component_.text = text_name_ + '\n';
        foreach (char letter in lines_[index_].ToCharArray())
        {
            text_component_.text += letter;
            yield return new WaitForSeconds(text_speed_); // Returns next element in collection
        }
    }
}