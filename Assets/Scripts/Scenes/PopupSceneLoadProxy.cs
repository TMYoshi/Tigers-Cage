using System.Collections.Specialized;
using UnityEngine;

public class PopupSceneLoadProxy : MonoBehaviour
{
    private CogUnlock consumeItem;
    [SerializeField] private string targetSceneName;
    [SerializeField] private string itemName = "WorkingCamera";

    public void CallFadeAndLoad()
    {
        if (InventoryManager.Instance != null)
        {
            bool itemRemoved = false;

            foreach(var slot in InventoryManager.Instance.itemSlot)
            {
                if (slot.isFull && slot.itemName.ToLower().Contains(itemName.ToLower()))
                {
                    slot.RemoveItem();
                    itemRemoved = true;
                    //Debug.Log(itemName + " removed.");
                    break;
                }
            }

            if (!itemRemoved)
            {
                Debug.LogWarning("Camera could not be removed from inventory.");
                return;
            }
        }
        

        if (SceneController.scene_controller_instance != null)
        {
            SceneController.scene_controller_instance.FadeAndLoadScene(targetSceneName); 
            // or trigger cutscene then flow into Hallway via CutsceneManager once animation is complete
        }
        else
        {
            Debug.LogError("SceneController not found, likely due to Popup + Door exit");
        }
    }

}
