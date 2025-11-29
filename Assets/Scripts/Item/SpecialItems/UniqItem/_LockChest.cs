using UnityEngine;
using System.Linq;
using TMPro;

public class _LockedChest : SpecialItems
{
    [SerializeField] BoxCollider2D colliderToDestroy;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openSprite;
    [SerializeField] GameObject CodeLock, OpenChest;
    public TMP_Text[] DisplayNumbers;
    public uint[] currentCode = { 0, 0, 0, 0 };
    uint[] correctCode = { 0, 8, 2, 8};

    bool _CompleteCondition, _ExitCondition;
    public void IncrementByOne(int _Location)
    {
        currentCode[_Location]++;
        if (currentCode[_Location] >= 10) currentCode[_Location] = 0;
        DisplayNumbers[_Location].text = currentCode[_Location].ToString();
    }
    public void CheckIfCorret()
    {
        if (currentCode.SequenceEqual(correctCode))
        {
            _CompleteCondition = true;
        }
        else
        {
            _ExitCondition = true;
        }
    }
    public override void EnterCondition()
    {
        CodeLock.SetActive(true);
        _CompleteCondition = false;
        _ExitCondition = false;
    }
    public override bool CompleteCondition()
    {
        //Add line of code to unlock page for jorurnal
        return _CompleteCondition;
    }

    public override bool ExitCondition()
    {
        return _ExitCondition;
    }

    public override void RewardCondition()
    {
        OpenChest.SetActive(true);
        gameObject.SetActive(false);
        Destroy(colliderToDestroy);
        //save system
    }

    public override void CleanUpCondition()
    {
        CodeLock.SetActive(false);
    }
}
