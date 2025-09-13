using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DisableEnable : MonoBehaviour
{
    [SerializeField] GameObject _object;

    public void Switch()
    {
        _object.SetActive(!_object.activeSelf);
    }

    public void Enable()
    {
        _object.SetActive(true);
    }

    public void Disable()
    {
        _object.SetActive(false);
    }

}
