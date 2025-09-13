using UnityEngine;
using UnityEngine.Events;

//this is for items with special events
public class SpecialItems : MonoBehaviour
{
    public UnityEvent OnClick;
    public void Hit()
    {
        OnClick.Invoke();
    }
}
