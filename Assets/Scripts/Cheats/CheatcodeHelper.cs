using UnityEngine;

public class CheatcodeHelper : MonoBehaviour
{
    public void Reset()
    {
        PlayerSaveHelper.WipeSave();
        SceneController.Instance.FadeAndLoadScene("Main Menu");
        InventoryManager.Instance.InventoryCleanup();
    }
}
