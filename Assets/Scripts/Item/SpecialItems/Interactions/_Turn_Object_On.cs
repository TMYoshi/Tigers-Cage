using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class _Turn_Object_On : SpecialItems
{
    [SerializeField] GameObject objectToTurnOn;
    public override void EnterCondition()
    {
    }
    public override bool CompleteCondition()
    {
        objectToTurnOn.SetActive(true);
        return true;
    }
    public override bool ExitCondition()
    {
        return false;
    }

    public override void RewardCondition()
    {

    }

    public override void CleanUpCondition()
    {

    }
}
