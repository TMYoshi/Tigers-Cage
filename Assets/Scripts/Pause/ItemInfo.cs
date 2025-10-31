using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameObject CardInfo;

    public void SwitchToCard()
    {
        CardInfo.SetActive(true);
    }
}
