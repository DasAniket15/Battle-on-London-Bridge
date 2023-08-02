using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform[] laserSpawnPoints;
    public float timeBetweenAttacks = 5f;
    public float laserDuration = 2f;
    public int minLasersToActivate = 1;
    public int maxLasersToActivate = 3;
    public GameObject warningEffectPrefab;

    private bool isAttacking = false;

    void Start()
    {
        // Start the laser attack pattern
        StartCoroutine(LaserAttackPattern());
    }

    IEnumerator LaserAttackPattern()
    {
        while (true)
        {
            // Wait for the time between attacks
            yield return new WaitForSeconds(timeBetweenAttacks);

            // Show the warning effect before the lasers spawn
            StartCoroutine(ShowWarningEffect());

            // Wait for a short delay before spawning the lasers
            yield return new WaitForSeconds(1.5f); // Adjust the delay as needed

            // Start the laser attack sequence
            isAttacking = true;

            // Randomly determine the number of lasers to activate
            int numLasersToActivate = Random.Range(minLasersToActivate, maxLasersToActivate + 1);

            // Randomly choose which spawn points to use for the laser attack
            Transform[] selectedSpawnPoints = new Transform[numLasersToActivate];
            ShuffleArray(laserSpawnPoints);
            for (int i = 0; i < numLasersToActivate; i++)
            {
                selectedSpawnPoints[i] = laserSpawnPoints[i];
            }

            // Activate the selected lasers
            foreach (Transform spawnPoint in selectedSpawnPoints)
            {
                StartCoroutine(LaserAttack(spawnPoint));
            }

            yield return new WaitForSeconds(laserDuration);
            Debug.Log("Destroy");
            isAttacking = false;
        }
    }

    IEnumerator ShowWarningEffect()
    {
        // Randomly determine the number of lasers to activate
        int numLasersToActivate = Random.Range(minLasersToActivate, maxLasersToActivate + 1);

        // Shuffle the spawn points array to randomize the laser spawn order
        ShuffleArray(laserSpawnPoints);

        // Choose a random subset of spawn points for the warning effect
        Transform[] warningSpawnPoints = new Transform[numLasersToActivate];
        for (int i = 0; i < numLasersToActivate; i++)
        {
            warningSpawnPoints[i] = laserSpawnPoints[i];
        }

        // Instantiate and show the warning effect at the selected spawn points
        foreach (Transform spawnPoint in warningSpawnPoints)
        {
            GameObject warningEffect = Instantiate(warningEffectPrefab, spawnPoint.position, Quaternion.identity);
            warningEffect.SetActive(true);
        }

        // Wait for the warning effect duration
        yield return new WaitForSeconds(1.5f); // Adjust the duration as needed

        // Destroy the warning effects
        foreach (Transform spawnPoint in warningSpawnPoints)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, 1f); // Adjust the radius as needed
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("WarningEffect"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }

    IEnumerator LaserAttack(Transform spawnPoint)
    {
        // Instantiate the laser prefab at the spawn point
        GameObject laserInstance = Instantiate(laserPrefab, spawnPoint.position, Quaternion.identity);

        // Move the laser down to the ground
        float elapsedTime = 0f;
        Vector3 startPosition = laserInstance.transform.position;
        Vector3 targetPosition = spawnPoint.position - Vector3.up * 10f; // Adjust the value to control laser length
        while (elapsedTime < laserDuration)
        {
            if (isAttacking == false)
            {
                // Stop the laser attack if the boss phase changes or attack is interrupted
               
                Destroy(laserInstance);
                yield break;
            }

            elapsedTime += Time.deltaTime;
            laserInstance.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / laserDuration);
            yield return null;
        }

        // Ensure the laser reaches the target position
        laserInstance.transform.position = targetPosition;
    }

    // Shuffle the array using Fisher-Yates shuffle algorithm
    private void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
