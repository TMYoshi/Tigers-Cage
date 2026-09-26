using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CogGlint : MonoBehaviour
{
    private float glintMin = 5f;
    private float glintMax = 12f;

    private ParticleSystem glintSystem;

    private void Awake()
    {
        glintSystem = GetComponentInChildren<ParticleSystem>();

        if(glintSystem == null)
        {
            Debug.LogError("No ParticleSystem found for Cog child.");
        }
    }


    void Start()
    {
        if (glintSystem != null)
        {
            StartCoroutine(RandomGlint());
        }
    }

    IEnumerator RandomGlint()
    {
        while (true)
        {
            float randomTime = Random.Range(glintMin, glintMax);
            yield return new WaitForSeconds(randomTime);

            TriggerGlint();
        }
    }

    void TriggerGlint()
    {
        glintSystem.Play();
    }
}
