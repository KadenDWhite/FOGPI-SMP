using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class Healing : MonoBehaviour
{
    public string tagToHeal;
    public int healed = 10;
    void OnTriggerEnter(Collider _other)
    {
        Health health = _other.GetComponent<Health>();

        if (health && _other.tag == tagToHeal)
        {
            health.Heal(healed);
        }
    }
}
