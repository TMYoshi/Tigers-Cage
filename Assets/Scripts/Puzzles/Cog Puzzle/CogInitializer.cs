using UnityEngine;

public class CogInitializer : MonoBehaviour
{
    // hard coded :^) values for positions and scales of cogs in tray
    private const float TRAY_Y_POSITION = -6f;

    private const float SMALL_TRAY_SCALE = 0.4f;
    private const float MEDIUM_TRAY_SCALE = 0.32f;
    private const float LARGE_TRAY_SCALE = 0.21f;

    private const float SMALL_HOVER_SCALE = 0.835f;
    private const float MEDIUM_HOVER_SCALE = 1.28f;
    private const float LARGE_HOVER_SCALE = 1.15f;

    public static class CogScales
    {
        public static float GetHoverScale(CogController.CogSize size)
        {
            switch (size)
            {
                case CogController.CogSize.Small: return SMALL_HOVER_SCALE;
                case CogController.CogSize.Medium: return MEDIUM_HOVER_SCALE;
                case CogController.CogSize.Large: return LARGE_HOVER_SCALE;
                default: return 1f; // Default case
            }
        }
    }

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
        SetCogProperties("Small Cogs/cog_small_1", -11f, SMALL_TRAY_SCALE); // -4, -1, 2 are x coords for small cogs in tray
        SetCogProperties("Small Cogs/cog_small_2", -8f, SMALL_TRAY_SCALE);
        SetCogProperties("Small Cogs/cog_small_3", -5f, SMALL_TRAY_SCALE);

        SetCogProperties("Medium Cogs/cog_medium_1", -2f, MEDIUM_TRAY_SCALE); // 5, 8 are x
        SetCogProperties("Medium Cogs/cog_medium_2", 1f, MEDIUM_TRAY_SCALE);

        SetCogProperties("Large Cogs/cog_large_1", 4f, LARGE_TRAY_SCALE);
    }

    private void SetCogProperties(string path, float x_position, float scale)
    {
        Transform cogTransform = cogParent.Find(path);

        if(cogTransform != null)
        {
            cogTransform.localPosition = new Vector3(x_position, TRAY_Y_POSITION, cogTransform.localPosition.z);

            cogTransform.localScale = Vector3.one * scale;

            CogController cogController = cogTransform.GetComponent<CogController>();
            if (cogController != null)
            {
                cogController.SetInitialProperties(cogTransform.localScale, cogTransform.position);
            }
        }
        else
        {
            Debug.LogError($"Cog not found at path: {path}.");
        }
    }

    
}
