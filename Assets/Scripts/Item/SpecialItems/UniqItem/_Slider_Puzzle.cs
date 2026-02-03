using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using System.Collections;

//code feels a little sloppy feel free to fix it up D:
public class _Slider_Puzzle : SpecialItems
{
	public GameObject exitButton;
	public Button exitButtonComponent;

    public Slider_Manager slider_Manager;
    public GameObject completed_card, actual_puzzle;
    public SpriteRenderer _renderer;
    public BoxCollider2D _collider;

    bool AllowForCompletion;
	bool ExitPuzzle;

	//if it already spawned the cards, no need to respawn the puzzle's entry conditions
	bool AlreadyShuffled = false;

	public void QueueToExitPuzzle()
		=> ExitPuzzle = true;

    public override void EnterCondition()
    {
		exitButton.SetActive(true);
		actual_puzzle.SetActive(true);

		AllowForCompletion = false;
		ExitPuzzle = false;

        if(!AlreadyShuffled) slider_Manager.EntryCondition();
		AlreadyShuffled = true;

        StartCoroutine(WaitUntilCheck());
    }

	float currentTime = 0;
	float lastPuzzlePieceShowSeconds = 3f;

    public override bool CompleteCondition()
    {
        slider_Manager.Puzzle();
        if (!AllowForCompletion) return false;
        if (!slider_Manager.CheckCompletion()) return false;
		exitButton.SetActive(false);

		currentTime += Time.deltaTime;

		if(currentTime > lastPuzzlePieceShowSeconds)
		{
			return true;
		}

		return false;
    }
    IEnumerator WaitUntilCheck()
    {
        yield return new WaitForSeconds(2);
        AllowForCompletion = true;
    }

    public override bool ExitCondition()
    {
		return ExitPuzzle;
    }

    public override void RewardCondition()
    {
        completed_card.SetActive(true);
        //line of code to set the document page to be turn on
        JournalDataManager.Instance.UnlockDocument(0); //add by Chris
        Destroy(_renderer);
        Destroy(_collider);
        // Add line of code to add new page for item documentation
    }

    public override void CleanUpCondition()
    {
		actual_puzzle.SetActive(false);
		exitButton.SetActive(false);
    }
}
