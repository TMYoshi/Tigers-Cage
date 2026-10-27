using UnityEngine;
using TMPro;


public class Date : MonoBehaviour
{

    public TextMeshProUGUI largeText;

    private float timer = 0f;

    public object DeltaTime { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        UpdateTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            UpdateTime();
            timer = 0f;
        }
    }

    // commented this out for now since warning flooding
    private void UpdateTime()
    {
        // largeText.text = System.DateTime.Now.ToString("2010 dd, yyyy hh:mm tt");
    }

}
