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

    private void OnDestroy()
    {
        // Stop the coroutine when the GameObject is destroyed
        isRunning = false;
    }

    private IEnumerator CastMagicCoroutine()
    {
        while (isRunning)
        {
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
}
