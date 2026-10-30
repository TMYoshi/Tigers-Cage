using UnityEngine;

public class Traversal_Button : MonoBehaviour
{
    public string proceed_to;

    // Function inheritted from MonoBehaviour: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseDown.html    
    private void OnMouseDown()
    {
        Debug.Log("Proceeding to " + proceed_to);
        SceneController.Instance.TraverseScene(proceed_to);
    }
}
