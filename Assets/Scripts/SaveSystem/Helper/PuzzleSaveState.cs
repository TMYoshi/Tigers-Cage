using UnityEngine;
using UnityEngine.Events;

public class PuzzleSaveState : MonoBehaviour
{
    [Header ("Unique ID for puzzle object")]
    [SerializeField] private string puzzleId;
    [SerializeField] UnityEvent AlreadySolved;

    private void Start()
    {
        //After loading, CollectedItems is resotred before start() runs and disables again to spefific Id
        //check if the puzzle id inside the collectedItems
        if (SaveData.Instance.AlreadyInteractedItems.Contains(puzzleId))
        {
            AlreadySolved.Invoke();
        }
    }

    //function is called when the puzzle is solved
    /*
    public void Complete()
    {
        if(InventoryManager.IsItemCollected(puzzleId))
            return;

        InventoryManager.MarkItemAsCollected(puzzleId);
        ApplyCompleteState();

        if(saveLoadManager != null)
        {
            saveLoadManager.SaveGame();
            Debug.Log("Puzzel Completed and saved");
        }
        else
        {
            Debug.LogWarning("Saveload managers is not assigned");
        }
    }
    //Check assign objects
    private void ApplyCompleteState()
    {
        if(objectsToDisable != null && objectsToDisable.Length > 0)
        {
            for(int i = 0; i < objectsToDisable.Length; i++)
            {
                if(objectsToDisable[i] != null)
                    objectsToDisable[i].SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    */
}
