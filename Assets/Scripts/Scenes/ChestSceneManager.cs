using UnityEngine;

public class ChestSceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PuzzlePathChecker.musicBoxPuzzleSolved)
        {
            GameObject room = GameObject.Find("Room Design");

            if (room != null)
            {
                Transform finishedBox = room.transform.Find("Music Box Finished");

                GameObject oldBox = GameObject.Find("Music Box");

                if (finishedBox != null) finishedBox.gameObject.SetActive(true);
                if (oldBox != null) oldBox.SetActive(false);
            }
        }
        if (CutsceneManager.musicBoxCutsceneCompleted)
        {
            GameObject room = GameObject.Find("Room Design");

            if (room != null)
            {
                Transform finishedBox = room.transform.Find("Music Box Finished");
                if (finishedBox != null) finishedBox.gameObject.SetActive(false);
            }
        }
    }
}
