using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
public class _LockedChest : SpecialItems
{
    private static HashSet<string> openedChests = new HashSet<string>();

    [SerializeField] string chestID = "MainRoomChest";
    [SerializeField] BoxCollider2D colliderToDestroy;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openSprite;
    [SerializeField] GameObject CodeLock, OpenChest;
    public TMP_Text[] DisplayNumbers;
    public uint[] currentCode = { 0, 0, 0, 0 };
    uint[] correctCode = { 0, 8, 2, 8 };
    bool _CompleteCondition, _ExitCondition;

    public override void Start()
    {
        base.Start();

        if (openedChests.Contains(chestID))
        {
            SetChestToOpen();
        }
    }

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
        if (openedChests.Contains(chestID)) { return; }

        CodeLock.SetActive(true);
        _CompleteCondition = false;
        _ExitCondition = false;
    }

    public override bool CompleteCondition()
    {
        if (openedChests.Contains(chestID)) return false;
        return _CompleteCondition;
    }

    public override bool ExitCondition()
    {
        if (openedChests.Contains(chestID)) return true;
        return _ExitCondition;
    }

    public override void RewardCondition()
    {
        openedChests.Add(chestID);
        SetChestToOpen();
    }

    public override void CleanUpCondition()
    {
        CodeLock.SetActive(false);
    }

    void SetChestToOpen()
    {
        OpenChest.SetActive(true);

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        if (colliderToDestroy != null)
        {
            Destroy(colliderToDestroy);
        }

        gameObject.SetActive(false);
    }
}