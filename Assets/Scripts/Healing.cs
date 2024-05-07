using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class Healing : MonoBehaviour
{
    public string allyTag; 
    public int healedAmount = 2; // Amount to heal per second
    public float healCooldown = 2f; // Cooldown between healing same ally
    public ParticleSystem healParticles;
    public Animator healAnimator;

    private float lastHealTime;
    private Health lastHealedAlly;

    void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health && other.CompareTag(allyTag) && other.gameObject != gameObject)
        {
            // Check if the ally is not currently being healed by another healer
            if (lastHealedAlly != health || Time.time - lastHealTime >= healCooldown)
            {
                HealAlly(health);
            }
        }
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