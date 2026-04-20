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

        useVideo = _videoPlayer != null && _videoPlayer.clip != null;

        if (useVideo)
        {
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
        // Countdown Associated Scenes - Turn off during cutscene
        if(Countdown.Instance.IsActive())
        {
            Debug.Log("Meow" + Countdown.is_active_);
            Countdown.Instance.gameObject.SetActive(false);
        }

        if (FadeController.Instance != null)
        {
            FadeController.Instance.onFadeInComplete -= StartCutscene;
        }

        if (this == null)
            return;

        StartCoroutine(PlayCutsceneSequence());
    }

    private void Update()
    {
        if (PlayerInput.Instance.SkipInput && !cutsceneFinished)
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
        // Countdown Related Logic - Turn back on when done
        if(Countdown.Instance.IsActive())
        {
            Countdown.Instance.gameObject.SetActive(true); // Game Object
            Countdown.is_active_ = true;
            Debug.Log("Bark " + Countdown.is_active_);
        }

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
