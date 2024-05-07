using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCaster : MonoBehaviour
{
    public float castInterval = 2f; // Interval between casting magic (in seconds)
    public GameObject magicPrefab; // Reference to the magic prefab to cast

    private bool isRunning = true; // Flag to control the coroutine

    private void Start()
    {
        // Start casting magic at regular intervals
        StartCoroutine(CastMagicCoroutine());
    }

    private IEnumerator CastMagicCoroutine()
    {
        while (isRunning)
        {
            // Check if there are any enemies in the scene
            if (!AreThereEnemiesWithTag("Red") && !AreThereEnemiesWithTag("Blue"))
            {
                // If no enemies are found, stop casting magic
                isRunning = false;
                yield break; // Exit coroutine
            }

            // Check if the magicPrefab is not null
            if (magicPrefab != null)
            {
                // Instantiate a new magic prefab at the current position and rotation
                Instantiate(magicPrefab, transform.position, transform.rotation);
            }

            // Wait for the specified interval before casting magic again
            yield return new WaitForSeconds(castInterval);
        }
    }

    // Function to check if there are enemies in the scene with the specified tag
    private bool AreThereEnemiesWithTag(string tag)
    {
        // Find all GameObjects with the specified tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        
        // If there are no GameObjects with the specified tag, return false
        return enemies.Length > 0;
    }
}