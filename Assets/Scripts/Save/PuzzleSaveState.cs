using UnityEditor.SearchService;
using UnityEngine;

public class PuzzleSaveState : MonoBehaviour
{
   [Header ("Unique ID for puzzle object")]
   [SerializeField] private string puzzleId;

    [Header ("Optional: objects to disable when completed")]
   [SerializeField] private GameObject[] objectsToDisable;

   private void Start()
    {
        //After loading, CollectedItems is resotred before start() runs and disables again to spefific Id
        //check if the puzzle id inside the collectedItems
        if (InventoryManager.IsItemCollected(puzzleId))
        {
            ApplyCompleteState();
        }
    }

    //function is called when the puzzle is solved
    public void Complete()
    {
        InventoryManager.MarkItemAsCollected(puzzleId);
        ApplyCompleteState();
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
}
