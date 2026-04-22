using UnityEngine;
using System.Collections.Generic;

public class HidingSpotManager : MonoBehaviour
{
    public static HidingSpotManager Instance;
    [Tooltip("Every hiding spot has a corresponding hiding spot")]
    [SerializeField] private GameObject item_gameobj_list_ = null;
    [SerializeField] private List<string> hiding_spots_;
    [SerializeField] private List<string> interactables_;
    public bool yes;

    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(this);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    private void Start()
    {
        if(hiding_spots_.Count == 0) { Debug.LogError("Hiding_spots empty!!");}
        if(hiding_spots_.Count != interactables_.Count) { Debug.LogError("Every hiding spot has a corresponding hiding spot"); }
    }

    // public void Update()
    // {
    //     if(yes)
    //     {
    //         SwapWithHidingSpot();
    //     }
    // }

    // public void AddHidingSpot(GameObject gameObject)
    // {
    //     string hiding_spot = gameObject.Find(gameObject.name);
    //     hiding_spots_.Add(hiding_spot);

    //     string interactable = gameObject.Find(gameObject.name - "Hidable");
    //     interactables_.Add(interactable);
    // }
    
    // Hint: Call on countdown
    public void SwapWithHidingSpot()
    {
        Debug.Log("koko");
        for(int i = 0; i < hiding_spots_.Count; ++i)
        {
            Transform hiding_spot = item_gameobj_list_.transform.Find(hiding_spots_[i] + "_Hidable");
            Transform interactable = item_gameobj_list_.transform.Find(interactables_[i]);
            
            if(hiding_spot != null && interactable != null) { 
                hiding_spot.gameObject.SetActive(true);
                interactable.gameObject.SetActive(false);
                return; // NOTE: Assuming single hiding spot / scene, change if multiple
            }
        }
    }

    // Hint: Call post heartbeat minigame
    public void SwapWithIteractable()
    {
        for(int i = 0; i < hiding_spots_.Count; ++i)
        {
            Transform hiding_spot = item_gameobj_list_.transform.Find(hiding_spots_[i] + "_Hidable");
            Transform interactable = item_gameobj_list_.transform.Find(interactables_[i]);
            
            if(hiding_spot != null && interactable != null) { 
                hiding_spot.gameObject.SetActive(false);
                interactable.gameObject.SetActive(true);
                return; // NOTE: Assuming single hiding spot / scene, change if multiple
            }
        }
    }

    // Hint: Call when moving to new room? Unsure cause the next rooms most likely have new spots manually assigned but will see
    public void CleanUp()
    {
        hiding_spots_.Clear();
        interactables_.Clear();
    }
}
