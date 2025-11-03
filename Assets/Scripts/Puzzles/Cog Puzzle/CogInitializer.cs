using UnityEngine;

public class CogInitializer : MonoBehaviour
{
    // hard coded :^) values for positions and scales of cogs in tray
    private const float TRAY_Y_POSITION = -6f;

    private const float SMALL_TRAY_SCALE = 0.4f;
    private const float MEDIUM_TRAY_SCALE = 0.32f;
    private const float LARGE_TRAY_SCALE = 0.21f;

    public Transform cogParent;

    void Start()
    {
        Transform cogParentTransform = transform.Find("Cogs");
        if(cogParentTransform == null)
        {
            Debug.LogError("'Cogs' parent not found in hierarchy");
            return;
        }
        cogParent = cogParentTransform;

        InitializeCogs();
    }

    private void InitializeCogs()
    {
        SetCogProperties("Small Cogs/cog_small_1", -4f, SMALL_TRAY_SCALE); // -4, 1, 2 are x coords for small cogs in tray
        SetCogProperties("Small Cogs/cog_small_2", -1f, SMALL_TRAY_SCALE);
        SetCogProperties("Small Cogs/cog_small_3", 2f, SMALL_TRAY_SCALE);

        SetCogProperties("Medium Cogs/cog_medium_1", 5f, MEDIUM_TRAY_SCALE); // 5, 8 are x
        SetCogProperties("Medium Cogs/cog_medium_2", 8f, MEDIUM_TRAY_SCALE);

        SetCogProperties("Large Cogs/cog_large_1", 11f, LARGE_TRAY_SCALE);
    }

    private void SetCogProperties(string path, float x_position, float scale)
    {
        Transform cogTransform = cogParent.Find(path);

        if(cogTransform != null)
        {
            cogTransform.localPosition = new Vector3(x_position, TRAY_Y_POSITION, cogTransform.position.z);

            cogTransform.localScale = Vector3.one * scale;
        }
        else
        {
            Debug.LogError($"Cog not found at path: {path}.");
        }
    }
}
