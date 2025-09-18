using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    [Header("Cutscene Settings")]
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GameObject skipUI;
    [SerializeField] private string nextSceneName = "MC Room - 1 North Wall"; // hard coded for now

    [Header("Alternative: Animation Cutscene")]
    [SerializeField] private Animator cutsceneAnimator;
    [SerializeField] private string animationTrigger = "PlayCutscene";

    private bool cutsceneFinished = false;
    private bool useVideo = true; // toggle b/w video and animation

    private void Awake()
    {
        Instance = this;

        // Add a listener to the FadeController's fade-in event
        if (FadeController.Instance != null)
        {
            FadeController.Instance.onFadeInComplete += StartCutscene;
        }

        useVideo = _videoPlayer != null && _videoPlayer.clip != null;
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
        // Unsubscribe from the event once it's triggered
        if (FadeController.Instance != null)
        {
            FadeController.Instance.onFadeInComplete -= StartCutscene;
        }

        // Now we can start the cutscene sequence, knowing the fade-in is complete
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
        // show skip UI
        if (skipUI != null)
        {
            skipUI.SetActive(true);
        }

        if (useVideo)
        {
            yield return StartCoroutine(PlayVideoCutscene());
        }
        else
        {
            yield return StartCoroutine(PlayAnimationCutscene());
        }

        // hide skip UI
        if (skipUI != null)
        {
            skipUI.SetActive(false);
        }

        ProceedToNextScene();
    }

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
}