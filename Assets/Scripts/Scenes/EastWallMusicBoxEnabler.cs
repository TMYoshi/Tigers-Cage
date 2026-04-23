using UnityEngine;

public class EastWallMusicBoxEnabler : MonoBehaviour
{
    void Start()
    {
        if (CutsceneManager.musicBoxCutsceneCompleted)
        {
            GameObject items = GameObject.Find("Items");
            GameObject collectibles = GameObject.Find("Collectable Items");

            if (items != null)
            {
                Transform brokenBox = items.transform.Find("Broken_Music_Box");
                if (brokenBox != null) brokenBox.gameObject.SetActive(true);
            }

            if (collectibles != null)
            {
                Transform batteries = collectibles.transform.Find("Batteries");
                //if (batteries != null) batteries.gameObject.SetActive(true);
            }
        }
    }
}