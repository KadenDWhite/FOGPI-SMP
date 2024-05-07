using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class Healing : MonoBehaviour
{
    public string allyTag; // Tag of the allies that can be healed
    public int healedAmount = 10;
    public ParticleSystem healParticles;
    public Animator healAnimator; // Reference to the Animator component

    private int numAlliesAlive;

    void Start()
    {
        // Count the number of allies alive at the start
        GameObject[] allies = GameObject.FindGameObjectsWithTag(allyTag);
        numAlliesAlive = allies.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        // Check if the collider has a Health component and is an ally
        if (health && other.CompareTag(allyTag))
        {
            // Check if the current object is not the healer itself
            if (other.gameObject != gameObject)
            {
                // Check if there are any lower health allies nearby
                Health lowestHealthAlly = FindLowestHealthAlly(allyTag);
                if (lowestHealthAlly != null && lowestHealthAlly != health)
                {
                    // Heal the lowest health ally
                    lowestHealthAlly.Heal(healedAmount);
                }
                else
                {
                    // No more allies to heal, stop the animator
                    StopAnimator();
                }
            }
        }
    }

    // Find the lowest health ally within the specified tag
    Health FindLowestHealthAlly(string tag)
    {
        GameObject[] allies = GameObject.FindGameObjectsWithTag(tag);
        Health lowestHealthAlly = null;
        float lowestHealth = float.MaxValue;

        foreach (GameObject ally in allies)
        {
            Health allyHealth = ally.GetComponent<Health>();
            if (allyHealth && allyHealth.currentHealth < lowestHealth)
            {
                lowestHealth = allyHealth.currentHealth;
                lowestHealthAlly = allyHealth;
            }
        }

        return lowestHealthAlly;
    }

    // Stop the animator
    void StopAnimator()
    {
        if (healAnimator != null)
        {
            healAnimator.enabled = false;
        }
    }
}