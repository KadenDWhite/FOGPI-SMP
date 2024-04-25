using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEmitter : MonoBehaviour
{
    public ParticleSystem magicParticlePrefab; // Assign the blue magic particle effect prefab in the Inspector
    public Transform staffCrystal; // Assign the staff crystal transform in the Inspector
    public float emitInterval = 2f; // Time interval between each emission

    private void Start()
    {
        // Start emitting particles every 2 seconds
        StartCoroutine(EmitMagicParticles());
    }

    private IEnumerator EmitMagicParticles()
    {
        while (true)
        {
            // Checks if the magicParticlePrefab and staffCrystal are assigned
            if (magicParticlePrefab != null && staffCrystal != null)
            {
                // Instantiate magic particles at the staff crystal's position
                ParticleSystem magicParticles = Instantiate(magicParticlePrefab, staffCrystal.position, Quaternion.identity);

                // Make sure the magic particles follow the staff crystal's position and rotation
                if (magicParticles != null)
                {
                    magicParticles.transform.SetParent(staffCrystal);
                    magicParticles.transform.localPosition = Vector3.zero;
                    magicParticles.transform.localRotation = Quaternion.identity;
                }
            }

            // Wait for the specified interval before emitting particles again
            yield return new WaitForSeconds(emitInterval);
        }
    }
}
