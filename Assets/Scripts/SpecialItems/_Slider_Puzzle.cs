using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class _Slider_Puzzle : SpecialItems
{
    public Slider_Manager slider_Manager;
    public override void EnterCondition()
    {
        slider_Manager.EntryCondition();
    }
    public override bool CompleteCondition()
    {
        return slider_Manager.CheckCompletion();
    }

    public override bool ExitCondition()
    {
        return false;
    }

    public override void RewardCondition()
    {
        // Add item to inventory
    }

    public override void CleanUpCondition()
    {
        Debug.Log("completed puzzle");
        // Delete card
    }
}
