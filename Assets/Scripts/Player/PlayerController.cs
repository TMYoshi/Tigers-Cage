using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] AnimationCurve animation_curve;
    public void GetOffscreenAndGoToMiddle(bool left)
    {
        //swaps x position util tool for getting the player offscreen
        transform.position = new Vector3((left ? -1 : 1) * 11, transform.position.y, transform.position.z);

        //goto middle
        GameObject _StartmoveObject = GameObject.Find("-MidpointPos");

        if(_StartmoveObject != null)
            MoveTo(_StartmoveObject.transform);
        else
            Debug.LogWarning("No -midposition for start to go towards");
    }
    public void MoveTo(Transform _moveTo, Action _onComplete = null)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToHelper(_moveTo, _onComplete));
    }
    public IEnumerator MoveToHelper(Transform _moveTo, Action _onComplete)
    {
        float originalPos = transform.position.x;
        float targetX = _moveTo.position.x;

        while (Mathf.Abs(transform.position.x - targetX) > 0.01f)
        {
            float progress = Mathf.Abs(transform.position.x - originalPos) / Mathf.Abs(originalPos - targetX);

            float newX = Mathf.MoveTowards(
                transform.position.x,
                targetX,
                Mathf.Clamp(animation_curve.Evaluate(progress), 0.01f, 1f) * speed * Time.deltaTime
            );

            transform.position = new Vector3(
                newX,
                transform.position.y,
                transform.position.z
            );

            yield return null;
        }

        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);

        if(_onComplete != null)
            _onComplete.Invoke();
    }
}
