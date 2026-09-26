using UnityEngine;

public class CutsceneSaveHelper : MonoBehaviour
{
    public void CutsceneSaveIndex(int _index)
    {
        SaveData.Instance.CutsceneSaved[_index] = true;
        SaveData.Instance.SavePlayer();
    }
}
