using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class Healing : MonoBehaviour
{
    public string allyTag; // Tag to identify allies (set to "Red" or "Blue")
    public float healingRange = 5f; // Range within which healing is considered
    public int healedAmount = 2; // Amount to heal per second
    public float healCooldown = 2f; // Cooldown between healing same ally
    public ParticleSystem healParticles;
    public Animator healAnimator;

    private float lastHealTime;
    private Health lastHealedAlly;

    void Update()
    {
        // If it's time to heal again and there's no ongoing healing process
        if (Time.time - lastHealTime >= healCooldown && lastHealedAlly == null)
        {
            GameObject target = FindTarget();
            if (target != null)
            {
                Health health = target.GetComponent<Health>();
                if (health)
                {
                    HealAlly(health);
                }
            }
        }
    }

    GameObject FindTarget()
    {
        GameObject target = null;

        // Find all objects with the specified tag
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(allyTag);

        // Sort possible targets by distance to healer
        System.Array.Sort(possibleTargets, (x, y) => Vector3.Distance(transform.position, x.transform.position)
                                                     .CompareTo(Vector3.Distance(transform.position, y.transform.position)));

        foreach(GameObject possibleTarget in possibleTargets)
        {
            Health health = possibleTarget.GetComponent<Health>();

            // Skip if the target is the healer itself
            if (possibleTarget == gameObject)
                continue;

            // Check if the target is within healing range
            if (Vector3.Distance(transform.position, possibleTarget.transform.position) <= healingRange)
            {
                if (health && (target == null || health.currentHealth < target.GetComponent<Health>().currentHealth))
                {
                    target = possibleTarget;
                }
            }
        }

        // If no other valid targets found, consider healing itself if within range
        if (target == null && Vector3.Distance(transform.position, transform.position) <= healingRange)
        {
            target = gameObject;
        }

        return target;
    }

    void HealAlly(Health allyHealth)
    {
        lastHealedAlly = allyHealth;
        lastHealTime = Time.time;
        StartCoroutine(HealOverTime(allyHealth));
    }

    IEnumerator HealOverTime(Health allyHealth)
    {
        while (allyHealth.currentHealth < allyHealth.maxHealth)
        {
            allyHealth.Heal(healedAmount);
            yield return new WaitForSeconds(1f); // Heal every second
        }
        lastHealedAlly = null; // Reset last healed ally
    }

    // Stop the animator and particles
    void StopAnimatorAndParticles()
    {
        if (healAnimator != null)
        {
            healAnimator.enabled = false;
        }
        if (healParticles != null)
        {
            healParticles.Stop();
        }
    }

    // Call this function when the healing process needs to stop
    public void StopHealing()
    {
        StopCoroutine("HealOverTime");
        StopAnimatorAndParticles();
    }
}