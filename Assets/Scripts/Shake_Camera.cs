using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ShakeCamera : MonoBehaviour
{
    public float duration_;
    public AnimationCurve curve_;
    // private bool do_once_ = true;
    public List<float> intervals_ = new List<float>();

    

    public IEnumerator WaitForShake()
    {
        Vector3 original_pos = transform.position;
        float current_time_ = 0;

        while(current_time_ < duration_)
        {
            Vector3 new_pos = Random.insideUnitCircle * curve_.Evaluate(current_time_ / duration_);
            new_pos.z = original_pos.z;
            transform.position = new_pos;

            current_time_ += Time.deltaTime;
            yield return null;
        }


        transform.position = original_pos;
    }
}
