using System.Collections;
using UnityEngine;


public class Laser : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform[] laserSpawnPoints;
    public float timeBetweenAttacks;
    public float laserDuration;
    public int minLasersToActivate;
    public int maxLasersToActivate;
    public GameObject warningEffectPrefab;
    private bool laserAllowed ;
    private static BossHealth bossHealth;

    private bool isAttacking = false;

    void Start()
    {
       
            Debug.Log("Start");
            StartCoroutine(LaserAttackPattern());
        


    }
    private void FixedUpdate()
    {
       
    }

    public void SetLaserBool(bool laserAllowed) 
    { 
        this.laserAllowed = laserAllowed;   
    }

    public IEnumerator LaserAttackPattern()
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

            yield return new WaitForSeconds(laserDuration);
            Debug.Log("Destroy");
            isAttacking = false;
        }
    }

    IEnumerator ShowWarningEffect()
    {
        // Start the laser attack sequence
        isAttacking = true;

        // Randomly determine the number of lasers to activate
        int numLasersToActivate = Random.Range(minLasersToActivate, maxLasersToActivate + 1);

        // Shuffle the spawn points array to randomize the laser spawn order
        ShuffleArray(laserSpawnPoints);

        // Choose a random subset of spawn points for the warning effect
        Transform[] selectedSpawnPoints = new Transform[numLasersToActivate];
        for (int i = 0; i < numLasersToActivate; i++)
        {
            selectedSpawnPoints[i] = laserSpawnPoints[i];
        }

        // Instantiate and show the warning effect at the selected spawn points
        foreach (Transform spawnPoint in selectedSpawnPoints)
        {
            GameObject warningEffect = Instantiate(warningEffectPrefab, spawnPoint.position, Quaternion.identity);
            warningEffect.SetActive(true);
        }

        // Wait for the warning effect duration
        yield return new WaitForSeconds(1.5f); // Adjust the duration as needed

        // Destroy all warning effects
        foreach (Transform spawnPoint in selectedSpawnPoints)
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

        // Wait for a short delay before spawning the lasers
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed

        // Spawn the lasers at the selected spawn points
        foreach (Transform spawnPoint in selectedSpawnPoints)
        {
            StartCoroutine(LaserAttack(spawnPoint));
        }

        // Wait for the laser duration
        yield return new WaitForSeconds(laserDuration);

        // End the laser attack sequence
        isAttacking = false;
    }

    IEnumerator LaserAttack(Transform spawnPoint)
    {
        // Instantiate the laser prefab at the spawn point
        GameObject laserInstance = Instantiate(laserPrefab, spawnPoint.position, Quaternion.Euler(0, 0, 90));

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
            yield return null;
        }

        // Ensure the laser reaches the target position
        laserInstance.transform.position = targetPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player got hit by the laser, reduce their health
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1f); // Reduce player health by 1
            }

            // TODO: Implement any visual/audio feedback for player getting hit
            Debug.Log("Laser hit player!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player got hit by the laser, reduce their health
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(0f); // Reduce player health by 1
            }

            // TODO: Implement any visual/audio feedback for player getting hit
            Debug.Log("This is a scam.");
        }
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
