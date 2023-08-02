using System.Collections;
using UnityEngine;

public class LaserWarning : MonoBehaviour
{
    public float warningDuration = 1.5f;

    void Start()
    {
        // Start the warning effect
        StartCoroutine(WarningEffect());
    }

    IEnumerator WarningEffect()
    {
        // Enable the particle system
        GetComponent<ParticleSystem>().Play();

        // Wait for the warning duration
        yield return new WaitForSeconds(warningDuration);

        // Stop the particle system and deactivate the warning object
        GetComponent<ParticleSystem>().Stop();
        gameObject.SetActive(false);
    }
}
