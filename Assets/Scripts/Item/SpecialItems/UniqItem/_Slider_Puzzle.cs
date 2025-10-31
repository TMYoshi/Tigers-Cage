using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class _Slider_Puzzle : SpecialItems
{
    public Slider_Manager slider_Manager;
    public GameObject completed_card, actual_puzzle;
    public SpriteRenderer _renderer;
    public BoxCollider2D _collider;
    bool AllowForCompletion = false;
    public override void EnterCondition()
    {
        slider_Manager.EntryCondition();
        StartCoroutine(WaitUntilCheck());
    }
    public override bool CompleteCondition()
    {
        slider_Manager.Puzzle();
        if (!AllowForCompletion) return false;
        return slider_Manager.CheckCompletion();
    }
    IEnumerator WaitUntilCheck()
    {
        yield return new WaitForSeconds(2);
        AllowForCompletion = true;
    }

    public override bool ExitCondition()
    {
        return false;
    }

    public override void RewardCondition()
    {
        Debug.Log("test");
        actual_puzzle.SetActive(false);
        completed_card.SetActive(true);
        Destroy(_renderer);
        Destroy(_collider);
        // Add line of code to add new page for item documentation
    }

    public override void CleanUpCondition()
    {
        // Delete card
    }
}
