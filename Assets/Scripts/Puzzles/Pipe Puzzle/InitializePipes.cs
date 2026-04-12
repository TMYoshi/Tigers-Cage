using UnityEngine;

public class InitializePipes : MonoBehaviour
{
    int[] rotationAmounts = { 0, 90, 180, 270};

     /*
     * solution:
     * 1, 90
     * 2, 180
     * 3, 270
     * 4, 180
     * 5, 90
     * 6, 90
     * 7, 90
     * 8, 0
     * 9, 0
     * 10, 180
     * 11, 90
     * 12, 180
     * 13, 270
     * 14, 180
     * 15, 90
     * 16, 270
     * 
     * 0: 8,9
     * 90: 1,5,6,7,11,15
     * 180: 2,4,10,12,14
     * 270: 3,13,16
     */

    void Start()
    {
        
    }
}
