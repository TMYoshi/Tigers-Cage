using UnityEngine;

public class EastWallMusicBoxEnabler : MonoBehaviour
{
    // more new scripts lmao sorry 
    void Start()
    {
        if (CutsceneManager.musicBoxCutsceneCompleted)
        {
            GameObject items = GameObject.Find("Items");

            if (items != null)
            {
                Transform brokenBox = items.transform.Find("Broken_Music_Box");
                if (brokenBox != null) brokenBox.gameObject.SetActive(true);
            }
        }
    }
}
