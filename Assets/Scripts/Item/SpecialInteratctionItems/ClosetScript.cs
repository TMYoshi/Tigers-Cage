using UnityEngine;

public class ClosetScript : MonoBehaviour
{
    public static bool ClosetState = false;
    [SerializeField] GameObject openCloset;
    [SerializeField] GameObject closeCloset;

    void Start()
    {
        openCloset.SetActive(ClosetState);
        closeCloset.SetActive(!ClosetState);
    }

    public void ToggleCloset()
    {
        ClosetState = !ClosetState;

        openCloset.SetActive(ClosetState);
        closeCloset.SetActive(!ClosetState);
    }
}
