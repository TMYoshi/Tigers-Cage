using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public GameObject SettingPanel;
    public void ToggleSetting()
    {
        if (SettingPanel.gameObject.activeSelf)
        {
            SettingPanel.gameObject.SetActive(false);
        }
        else
        {
            SettingPanel.gameObject.SetActive(true);
        }
    }
}
