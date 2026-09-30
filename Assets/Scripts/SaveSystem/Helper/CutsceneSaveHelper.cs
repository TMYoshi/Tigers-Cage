using UnityEngine;

public class CutsceneSaveHelper : MonoBehaviour
{
    public void RegisterScene()
    {
        if(SaveData.Instance.SceneIndex != "")
        {
            GetComponent<CutsceneManager>().nextSceneName = SaveData.Instance.SceneIndex;
        }
    }
    public void CutsceneSaveIndex(int _index)
    {
        SaveData.Instance.CutsceneSaved[_index] = true;
        SaveData.Instance.SavePlayer();
    }
}
