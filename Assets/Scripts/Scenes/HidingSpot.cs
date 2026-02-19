using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] private bool is_valid_spot_;
    // TODO: [SerializeField] private Cutscene failure_cutscene;
    public bool IsValidSpot()
    {
        return is_valid_spot_;
    }
}
