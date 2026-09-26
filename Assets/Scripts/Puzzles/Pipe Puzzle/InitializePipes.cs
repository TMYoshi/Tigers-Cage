using UnityEngine;

public class InitializePipes : MonoBehaviour
{
    [SerializeField] private Transform pipesParent;

    int[] rotationAmounts = { 0, 90, 180, 270 };

    /*
    Solution:
    90
    0
    180
    0
    0

    90
    180
    0
    180
    270

    90
    90
    180
    0
    90

    270
    270
    90
    270
    270

    270
    180
    180
    180
    270

    90:  1,6,11,12,15,18
    180: 3,7,9,13,22,23,24
    270: 10,16,17,19,20,21,25
    0:   2,4,5,8,14
    */

    private void Start()
    {
        RandomizePipes();
    }

    public void RandomizePipes()
    {
        if (pipesParent == null)
        {
            Transform foundParent = transform.parent?.Find("Pipes");
            if (foundParent != null) pipesParent = foundParent;
            else return;
        }

        float[] pipeZRotations = new float[pipesParent.childCount];

        for (int i = 0;  i < pipesParent.childCount; i++)
        {
            int randomIndex = Random.Range(0, 4);
            int randomZRotation = rotationAmounts[randomIndex];
            Transform pipe = pipesParent.GetChild(i);

            if (pipe.TryGetComponent<PipeRotation>(out PipeRotation pipeScript))
            {
                pipeScript.SetRotation(randomZRotation);
            }

            Vector3 currentEuler = pipe.eulerAngles;
            pipe.eulerAngles = new Vector3(currentEuler.x, currentEuler.y, randomZRotation);

            pipeZRotations[i] = pipe.eulerAngles.z;
        }
    }
}
