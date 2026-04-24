using UnityEngine;
using System.Linq;
using TMPro;

public class _LockedChest : SpecialItems
{
    [SerializeField] BoxCollider2D colliderToDestroy;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openSprite;
    [SerializeField] GameObject CodeLock, OpenChest;
	[SerializeField] GameObject ExitButtonCanvas;
    [SerializeField] AudioClip unlock_sound_;
    [SerializeField] AudioClip padlock_sound_;
    public TMP_Text[] DisplayNumbers;
    public uint[] currentCode = { 0, 0, 0, 0 };
    uint[] correctCode = {0,8,2,8};
    // remind me to change lol-n
    bool _CompleteCondition, _ExitCondition;

	public override void Start()//
	{
		item.AssignSpecialEvents(this);
		if(InventoryManager.alreadyInteratedItems.Contains("Chest"))
		{
			RewardCondition();
		}
	}

	public void SetExitCondition(bool _Condition) =>
		_ExitCondition = _Condition;

    public void IncrementByOne(int _Location)
    {
        SFXManager.Instance.PlaySFXClip(padlock_sound_);
        
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
    }
    public override void EnterCondition()
    {
		CodeLock.SetActive(true);
		_CompleteCondition = false;
		_ExitCondition = false;
		ExitButtonCanvas.SetActive(true);
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
		InventoryManager.alreadyInteratedItems.Add("Chest");

        OpenChest.SetActive(true);
        SFXManager.Instance.PlaySFXClip(unlock_sound_);
        gameObject.SetActive(false);
        Destroy(colliderToDestroy);
        //save system noted for chris 
    }

    public override void CleanUpCondition()
    {
        CodeLock.SetActive(false);
		ExitButtonCanvas.SetActive(false);
    }
}
