using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

// { 5.928f, 4.196f, 2.46f, 0.732f, -1.0f, -2.732f, -4.464f }; // gap of 1.732 = sqrt of 3; (1^2 + 1.732^2) = 2^2 

public class AxleManager : MonoBehaviour
{
    public static AxleManager Instance { get; private set; }

    // max distance a cog can be from an axle to snap onto
    public float snapRange = 1.0f;

    // axle coordinates 
    private List<Vector3> validAxlePositions = new List<Vector3>();

    // occupied positions
    // position -> CogController occupying it
    private Dictionary<Vector3, CogController> occupiedPositions = new Dictionary<Vector3, CogController>();

    private Dictionary<Vector3, Dictionary<CogController.CogSize, float>> axleRotationOffsets = 
        new Dictionary<Vector3, Dictionary<CogController.CogSize, float>>();

    private readonly Dictionary<CogController.CogSize, float> sizeRotationOffsets =
        new Dictionary<CogController.CogSize, float>()
    {
            {CogController.CogSize.Small, 30f },
            {CogController.CogSize.Medium, 15f },
            {CogController.CogSize.Large, 7.5f }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ScanForAxlePositions();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void ScanForAxlePositions()
    {
        axleRotationOffsets.Clear();
        validAxlePositions.Clear();
        recursiveFindAxles(transform);
        Debug.Log($"AxleManager found {validAxlePositions.Count} active, valid snap positions.");
    }

    private void recursiveFindAxles(Transform parent)
    {
        Transform axlesParentTransform = transform;

        foreach(Transform child in parent)
        {
            bool isAxle = (child.name.StartsWith("Axle") && child.name != "Axles") ||
                         child.name.StartsWith("fixed_");

            if (isAxle)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    Vector3 worldPosition = child.position;
                    Vector3 axleLocalPosition = axlesParentTransform.InverseTransformPoint(worldPosition);

                    validAxlePositions.Add(axleLocalPosition);
                    CalculateRotationOffsetsForAxle(axleLocalPosition);
                }
            }

            recursiveFindAxles(child);
        }
    }

    private void CalculateRotationOffsetsForAxle(Vector3 axlePosition)
    {
        axleRotationOffsets[axlePosition] = new Dictionary<CogController.CogSize, float>();
        float verticalSpacing = 1.732f; // sqrt 3 
        float horizontalSpacing = 2.0f;

        int col = Mathf.RoundToInt(axlePosition.x / horizontalSpacing);
        int row = Mathf.RoundToInt(axlePosition.y / verticalSpacing);
        
        bool shouldOffset = (col + row) % 2 != 0;

        foreach (var sizeOffset in sizeRotationOffsets)
        {
            CogController.CogSize cogSize = sizeOffset.Key;
            float rotationAmount = sizeOffset.Value;

            float finalRotation = shouldOffset ? rotationAmount : 0f;
            axleRotationOffsets[axlePosition][cogSize] = finalRotation;
        }
    }

    public Vector3 FindNearestAvailablePosition(Vector3 releasePosition)
    {
        Vector3 nearestPosition = Vector3.zero;
        float shortestDistance = snapRange;

        foreach(Vector3 axlePos in validAxlePositions)
        {
            float distance = Vector3.Distance(releasePosition, axlePos);

            if(distance < shortestDistance)
            {
                if (!occupiedPositions.ContainsKey(axlePos))
                {
                    shortestDistance = distance;
                    nearestPosition = axlePos;
                }
            }
        }
        return nearestPosition;
    }

    public void OccupyPosition(Vector3 position, CogController cog)
    {
        occupiedPositions[position] = cog;
    }

    public void ReleasePosition(Vector3 position)
    {
        occupiedPositions.Remove(position);
    }

    public float GetAxleRotationOffset(Vector3 axlePosition, CogController.CogSize cogSize)
    {
        if(axleRotationOffsets.ContainsKey(axlePosition) &&
            axleRotationOffsets[axlePosition].ContainsKey(cogSize))
        {
            return axleRotationOffsets[(axlePosition)][cogSize];
        }
        Debug.LogWarning($"No rotation offset for axle at {axlePosition} with size {cogSize}");
        return 0f;
    } 
}
