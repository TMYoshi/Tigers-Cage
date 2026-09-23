using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class VideoLoader : MonoBehaviour
{
    public string pathDir;
    VideoPlayer player;

    void Awake()
    {
        player = GetComponent<VideoPlayer>();

        player.source = VideoSource.Url;
        player.url = Path.Combine(
            Application.streamingAssetsPath,
            pathDir
        );
    }
}
