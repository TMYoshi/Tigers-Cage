using System.Collections.Generic;
using UnityEngine;

public class PuzzlePathChecker : MonoBehaviour
{
    

    public static PuzzlePathChecker Instance { get; private set; }

    // must be assigned; can change for future puzzles
    [Header("Key Cogs")]
    public CogController driverCog;
    public CogController upperEndCog;
    public CogController lowerEndCog;

    private Dictionary<CogController, HashSet<CogController>> connectionGraph = 
        new Dictionary<CogController, HashSet<CogController>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if(driverCog == null)
        {
            GameObject driver = GameObject.Find("cog_small_fixed_start");
            if(driver != null) driverCog = driver.GetComponent<CogController>();
        }
        if (upperEndCog == null)
        {
            GameObject upper = GameObject.Find("cog_small_fixed_1");
            if (upper != null) upperEndCog = upper.GetComponent<CogController>();
        }
        if (lowerEndCog == null)
        {
            GameObject lower = GameObject.Find("cog_medium_fixed_2");
            if (lower != null) lowerEndCog = lower.GetComponent<CogController>();
        }
        if(driverCog == null || upperEndCog == null || lowerEndCog == null)
        {
            Debug.LogError("PuzzlePathChecker has missing key cog references! Assign in inspector.");
        }
        RebuildConnectionGraph();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) CheckPuzzleCompletion();
    }

    public void OnCogPlaced(CogController cog)
    {
        if (cog == null) return;

        RebuildConnectionGraph();

        Debug.Log($"Graph updated with: {cog.name}");
    }

    private int GetToothCount(CogController.CogSize size)
    {
        switch (size)
        {
            case CogController.CogSize.Small: return 6;
            case CogController.CogSize.Medium: return 12;
            case CogController.CogSize.Large: return 24;

            default: return 12;
        }
    }

    private float GetHalfPitch(CogController.CogSize size)
    {
        // rotate: gap, tooth, gap, etc
        if (size == CogController.CogSize.Small) return 30f;
        if (size == CogController.CogSize.Medium) return 15f;
        if (size == CogController.CogSize.Large) return 7.5f;
        return 0f;
    }

    private float CalculateMeshRotation(CogController parent, CogController child)
    {
        // angle from parent to child, where cogs touch
        Vector3 direction = child.transform.position - parent.transform.position;
        float angleToChild = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // cog ratios based on sizes/teeth
        float parentTeeth = GetToothCount(parent.size);
        float childTeeth = GetToothCount(child.size);
        float ratio = parentTeeth / childTeeth;

   
        float parentRotationAtContact = Mathf.DeltaAngle(angleToChild, parent.currentRotation);
        float childRotationAtContact = (parentRotationAtContact * -ratio) + 180f + GetHalfPitch(child.size);
        return Mathf.Repeat(angleToChild + childRotationAtContact, 360f);
    }

    private void AlignAllCogs()
    {
        if (driverCog == null) return;

        CogController[] allCogs = FindObjectsByType<CogController>(FindObjectsSortMode.None);
        foreach (var c in allCogs)
        {
            c.isAligned = false;
        }

        Queue<CogController> queue = new Queue<CogController>();

        driverCog.isAligned = true;
        driverCog.transform.rotation = Quaternion.Euler(0, 0, driverCog.currentRotation);
        queue.Enqueue(driverCog);

        while (queue.Count > 0)
        {
            CogController parent = queue.Dequeue();

            if (!connectionGraph.ContainsKey(parent)) continue;

            foreach (CogController child in connectionGraph[parent])
            {
                if (child.isAligned) continue;

                child.currentRotation = CalculateMeshRotation(parent, child);
                child.transform.rotation = Quaternion.Euler(0, 0, child.currentRotation);

                child.isAligned = true;
                queue.Enqueue(child);
            }
        }
    }

    public void OnCogRemoved(CogController cog)
    {
        if (cog == null) return;

        if (connectionGraph.ContainsKey(cog))
        {
            foreach(var neighbor in connectionGraph[cog])
            {
                if(neighbor != null && connectionGraph.ContainsKey(neighbor))
                {
                    connectionGraph[neighbor].Remove(cog);
                }
            }
            connectionGraph.Remove(cog);
        }
        //Debug.Log($"Cog removed: {cog.name}");
        CheckPuzzleCompletion();
    }
    private void CheckPuzzleCompletion()
    {
        if(driverCog == null || upperEndCog == null || lowerEndCog == null)
        {
            Debug.Log("Can't check for completion, puzzle is missing key cog references.");
            return;
        }

        bool upperPathExists = HasPath(driverCog, upperEndCog);
        bool lowerPathExists = HasPath(driverCog, lowerEndCog);

        if (upperPathExists && lowerPathExists)
        {
            OnPuzzleSolved();
        }
    }

    private bool HasPath(CogController start, CogController end)
    {
        if (start == null || end == null) return false;
        if (!connectionGraph.ContainsKey(start)) return false;

        Queue<CogController> queue = new Queue<CogController>();
        HashSet<CogController> visited = new HashSet<CogController>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            CogController current = queue.Dequeue();
            if (current == end) return true;
            if (!connectionGraph.ContainsKey(current)) continue;

            foreach(var neighbor in connectionGraph[current])
            {
                if (neighbor == null || visited.Contains(neighbor)) continue;
                visited.Add(neighbor);
                queue.Enqueue(neighbor);
            }
        }
        return false;
    }

    private List<CogController> GetMeshedNeighbors(CogController cog)
    {
        List<CogController> neighbors = new List<CogController>();
        if (cog == null || cog.outerCollider == null) return neighbors;

        ContactFilter2D filter = ContactFilter2D.noFilter;
        List<Collider2D> overlapResults = new List<Collider2D>();
        int count = cog.outerCollider.Overlap(filter, overlapResults);

        if (count > 0)
        {
            foreach (Collider2D otherCollider in overlapResults)
            {
                if (otherCollider == cog.innerCollider || otherCollider == cog.outerCollider) continue;
                CogController otherCog = otherCollider.GetComponent<CogController>();
                if (otherCog != null && otherCollider == otherCog.outerCollider)
                {
                    neighbors.Add(otherCog);
                }
            }
        }
        return neighbors;
    }

    private void OnPuzzleSolved()// notes for chris puzzle solved
    {
        Debug.LogWarning($"puzzle solved");
        SceneController.scene_controller_instance.FadeAndLoadSceneWithCutscene("Music_Box_Cutscene", "MC Room - 1 North Wall");
    }

    private void RebuildConnectionGraph()
    {
        connectionGraph.Clear();
        CogController[] allCogs = FindObjectsByType<CogController>(FindObjectsSortMode.None);

        foreach(var cog in allCogs)
        {
            if (cog == null) continue;
            List<CogController> neighbors = GetMeshedNeighbors(cog);

            if(neighbors.Count > 0 || cog.name.Contains("fixed"))
            {
                if (!connectionGraph.ContainsKey(cog))
                {
                    connectionGraph[cog] = new HashSet<CogController>();
                }
                foreach (var neighbor in neighbors)
                {
                    connectionGraph[cog].Add(neighbor);
                }
            }
        }
        Debug.Log($"Connection graph rebuilt with {connectionGraph.Count} cogs.");

        AlignAllCogs();
        CheckPuzzleCompletion();
    }
}