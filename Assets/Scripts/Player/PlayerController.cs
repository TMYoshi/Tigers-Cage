using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxX, minX;
    [Tooltip("the player won't move if the click is within this min ")]
    [SerializeField] float minXMove;
    [SerializeField] Transform flipTransform;
    [SerializeField] float speed;
    [SerializeField] float teleportBackY = -2.0f;
    //false = left
    //true = right 
    public UnityEvent<bool> OnWalk;
    public UnityEvent OnStop;

    public void FlipBaseOnDirection(bool left)
    {
        if(!left)
            flipTransform.rotation = Quaternion.Euler(0, 0, 0);
        else
            flipTransform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void GetOffscreenAndGoToMiddle(bool left)
    {
        //swaps x position util tool for getting the player offscreen
        transform.position = new Vector3((left ? -1 : 1) * 5, transform.position.y, transform.position.z);

        //goto middle
        GameObject _StartmoveObject = GameObject.Find("-MidpointPos");

        if(_StartmoveObject != null)
            MoveTo(_StartmoveObject.transform);
        else
            Debug.LogWarning("No -midposition for start to go towards");
    }

    void Start()
    {
        if(SceneController.Instance.shouldMoveToMiddle == false)
        {
            transform.position = new Vector3(SceneController.Instance.teleportPositionForGoingBack.x, teleportBackY, 0f);
            SceneController.Instance.shouldMoveToMiddle = true;
            return;
        }

        if(SceneController.Instance == null)
        {
            Debug.LogWarning("No Scene Controller D:");
            GetOffscreenAndGoToMiddle(false);
        }
        else
            GetOffscreenAndGoToMiddle(SceneController.Instance.lastArrowClicked == "Right" ? true : false);
    }

    public static void WalkToOnClick(PlayerController _playerController)
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(PlayerInput.Instance.MouseInput);
        targetPos.z = -10;

        _playerController.MoveTo
        (
            targetPos
        );
    }

    public static void StopMoving(PlayerController _playerController)
    {
        if(_playerController == null) return;
        _playerController.StopAllCoroutines();
        _playerController.MoveTo
        (
            _playerController.transform.position
        );
    }

    public void MoveTo(Transform _moveTo, Action _onComplete = null)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToHelper(_moveTo.position, _onComplete));
    }

    public void MoveTo(Vector3 _moveTo, Action _onComplete = null)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToHelper(_moveTo, _onComplete));
    }

    public IEnumerator MoveToHelper(Vector3 _moveTo, Action _onComplete)
    {
        float originalPos = transform.position.x;
        float targetX = _moveTo.x;

        if(minXMove > Mathf.Abs(originalPos - targetX))
        {
            if(!PauseMenu.Instance.IsPaused)
                _onComplete?.Invoke();

            OnStop?.Invoke();
            yield break;
        }


        OnWalk?.Invoke(originalPos - targetX < 0 ? true : false);

        if(targetX > maxX)
            targetX = maxX;

        if(targetX < minX)
            targetX = minX;

        while (Mathf.Abs(transform.position.x - targetX) > 0.01f)
        {
            float progress = Mathf.Abs(transform.position.x - originalPos) / Mathf.Abs(originalPos - targetX);

            float newX = Mathf.MoveTowards(
                transform.position.x,
                targetX,
                speed * Time.deltaTime
            );

            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );

            yield return null;
        }

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);

        if(!PauseMenu.Instance.IsPaused)
            _onComplete?.Invoke();

        OnStop?.Invoke();
    }
}
