using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    public static bool musicBoxCutsceneCompleted;
    public static CutsceneManager Instance;

    [Header("Cutscene Settings")]
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GameObject skipUI;
    public string nextSceneName = "GameOver";

    [Header("Alternative: Animation Cutscene")]
    [SerializeField] private Animator cutsceneAnimator;
    [SerializeField] private string animationTrigger = "PlayCutscene";

    [Header("Save System")]
    [SerializeField] int skipIndex;

    [Header("Events")]
    public UnityEvent OnCutsceneComplete;

    private bool cutsceneFinished = false;
    private bool useVideo = true;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if(SaveData.Instance.SceneIndex != "")
        {
            nextSceneName = SaveData.Instance.SceneIndex;
        }

        if(SaveData.Instance.CutsceneSaved[skipIndex])
        {
            SkipCutscene();
            return;
        }

        StartCutscene();
    }

    //bool startedCutscene = false;
    private void StartCutscene()
    {
        Debug.Log("StartCutscene Called!");

        if (Countdown.Instance != null && Countdown.Instance.IsActive())
        {
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

        useVideo = _videoPlayer != null && (_videoPlayer.clip != null || !string.IsNullOrEmpty(_videoPlayer.url));

        if (useVideo)
        {
            _videoPlayer.loopPointReached -= OnVideoFinished;
            _videoPlayer.loopPointReached += OnVideoFinished;

            _videoPlayer.Play();
            yield break;
        }
        else
        {
            yield return StartCoroutine(PlayAnimationCutscene());
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

        if (skipUI != null)
        {
            skipUI.SetActive(false);
        }

        ProceedToNextScene();
    }

    public void SkipCutscene()
    {
        if (cutsceneFinished) return;

        Debug.Log("Cutscene skipped.");

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

    public void ProceedToNextScene()
    {
        if (cutsceneFinished) return;
        cutsceneFinished = true;

        if (skipUI != null)
        {
            skipUI.SetActive(false);
        }

        string currentScene = SceneManager.GetActiveScene().name;

        if (SceneController.scene_controller_instance != null)
        {
            SceneController.scene_controller_instance.FadeAndLoadScene(nextSceneName);
            OnCutsceneComplete?.Invoke();

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
        if (FadeController.Instance != null)
        {
            FadeController.Instance.onFadeInComplete -= StartCutscene;
        }

        if (useVideo && _videoPlayer != null)
        {
            _videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
