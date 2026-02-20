using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    [Header("Cutscene Settings")]
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GameObject skipUI;
    [SerializeField] private string nextSceneName = "GameOver";

    [Header("Alternative: Animation Cutscene")]
    [SerializeField] private Animator cutsceneAnimator;
    [SerializeField] private string animationTrigger = "PlayCutscene";

    private bool cutsceneFinished = false;
    private bool useVideo = true;

    private void Awake()
    {
        Instance = this;

        if (FadeController.Instance != null)
        {
            FadeController.Instance.onFadeInComplete += StartCutscene;
        }
        
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "intro_cutscene.mp4");

        if (SceneManager.GetActiveScene().name == "Ending_Cutscene")
        {
            videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "cutscene_music_box.mp4");
        }

        useVideo = _videoPlayer != null;
        _videoPlayer.url = videoPath;

        if (useVideo)
        {
            // Subscribe to the video finished event
            _videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    private void Start()
    {
        string storedNextScene = PlayerPrefs.GetString("NextSceneAfterCutscene", "");
        if (!string.IsNullOrEmpty(storedNextScene))
        {
            nextSceneName = storedNextScene;
            PlayerPrefs.DeleteKey("NextSceneAfterCutscene");
        }
    }

    private void StartCutscene()
    {
        if (FadeController.Instance != null)
        {
            FadeController.Instance.onFadeInComplete -= StartCutscene;
        }

        StartCoroutine(PlayCutsceneSequence());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !cutsceneFinished)
        {
            SkipCutscene();
        }
    }

    private IEnumerator PlayCutsceneSequence()
    {
        if (skipUI != null)
        {
            skipUI.SetActive(true);
        }

        if (useVideo)
        {
            _videoPlayer.Play();
            // The rest of the logic is now handled by the OnVideoFinished event, so we can exit this coroutine for video
            yield break;
        }
        else
        {
            yield return StartCoroutine(PlayAnimationCutscene());
        }

        if (skipUI != null)
        {
            skipUI.SetActive(false);
        }

        ProceedToNextScene();
    }

    // This method is now obsolete for video cutscenes
    private IEnumerator PlayVideoCutscene()
    {
        _videoPlayer.Play();

        while (_videoPlayer.isPlaying && !cutsceneFinished)
        {
            yield return null;
        }
        _videoPlayer.Stop();
    }

    private IEnumerator PlayAnimationCutscene()
    {
        if (cutsceneAnimator != null)
        {
            cutsceneAnimator.SetTrigger(animationTrigger);
            yield return new WaitForSeconds(0.1f);

            while (cutsceneAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f && !cutsceneFinished)
            {
                yield return null;
            }
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished playing. Proceeding to next scene.");
        ProceedToNextScene();
    }

    public void SkipCutscene()
    {
        if (cutsceneFinished) return;

        Debug.Log("Cutscene skipped.");
        cutsceneFinished = true;

        if (useVideo && _videoPlayer.isPlaying)
        {
            _videoPlayer.Stop();
        }

        if (!useVideo && cutsceneAnimator != null)
        {
            cutsceneAnimator.SetTrigger("SkipCutscene");
        }

        ProceedToNextScene();
    }

    private void ProceedToNextScene()
    {
        cutsceneFinished = true;

        if (SceneController.scene_controller_instance != null)
        {
            SceneController.scene_controller_instance.FadeAndLoadScene(nextSceneName);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void OnSkipButtonPressed()
    {
        SkipCutscene();
    }

    private void OnDestroy()
    {
        if (useVideo && _videoPlayer != null)
        {
            _videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
