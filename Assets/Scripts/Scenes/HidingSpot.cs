using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] private bool is_hiding_spot_;
    public bool IsHidingSpot()
    {
        return is_hiding_spot_;
    }
}
