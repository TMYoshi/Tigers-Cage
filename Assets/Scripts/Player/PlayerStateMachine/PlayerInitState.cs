using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInitState : MonoBehaviour
{
    public void PlayerOnStart()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log(SceneManager.GetActiveScene().name);
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Idle);

        if(SaveData.Instance.CollectedItemIds.Contains("MC Room - 3 South Wall_Rabbit_"))
        {
            PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Idle);
            PlayerAnimator.SetBunnyTrue();
        }
        else if(_scene.name == "MC Room - 3 South Wall")
        {
            PlayerStateManager.Instance.UpdateCurrentState(PlayerStateManager.State.Nervous);
        }
    }
}
