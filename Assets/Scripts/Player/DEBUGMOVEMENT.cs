using UnityEngine;
using System.Collections;

public class DEBUGMOVEMENT : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    void Start()
    {
        controller.MoveTo(transform, () => Debug.Log("Finished Moving"));
    }
}
