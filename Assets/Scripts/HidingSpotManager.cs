using UnityEngine;
using System.Collections;

public class HidingSpotManager : MonoBehaviour
{
    public static HidingSpotManager Instance;
    [Tooltip("Every hiding spot has a corresponding hiding spot")]
    private List<GameObject> hiding_spots_;
    private List<GameObject> interactables_;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void AddHidingSpot(GameObject gameObject)
    {
        hiding_spots_.Add(gameObject.Find(gameObject));
        interactables_.Add(gameObject.Find(gameObject - "Hidable"));
    }
    
    public void SwapWithHidingSpot()
    {
        for(int i = 0; i < hiding_spots_.Count; ++i)
        {
            if(hiding_spots_[i] != null) { hiding_spots_[i].gameObject.SetActive(true); }
            if(interactables_[i] != null) { interactables_[i].gameObject.SetActive(false); }
        }
    }

    public void SwapWithIteractable()
    {
        for(int i = 0; i < hiding_spots_.Count; ++i)
        {
            if(hiding_spots_[i] != null) { hiding_spots_[i].gameObject.SetActive(false); }
            if(interactables_[i] != null) { interactables_[i].gameObject.SetActive(true); }
        }
    }
}
