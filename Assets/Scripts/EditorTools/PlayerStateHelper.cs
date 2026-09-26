#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class PlayerStateHelper : EditorWindow
{
    bool idleConstant = false;
    [MenuItem("Tiger Tools/State Helper")]
    public static void ShowWindow()
    {
        GetWindow<PlayerStateHelper>("State Helper");
    }

    private void OnGUI()
    {
        idleConstant = GUILayout.Toggle(idleConstant, "IDLE");
        if (idleConstant)
        {
            PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Idle);
        }
    }
}
#endif

