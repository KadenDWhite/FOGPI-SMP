using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public float range = 20.0f;
    public string targetTag;
    public GameObject FindTarget()
    {
        GameObject target = null;

        // Find all objects with the specified tag
        List<GameObject> possibleTargets = GameObject.FindGameObjectsWithTag(targetTag).ToList<GameObject>();

        // Remove the attacker from the list of possible targets
        possibleTargets.Remove(gameObject); // Assuming the attacker is attached to the GameObject using this script

        float closestDistance = float.MaxValue;
        foreach(GameObject pt in possibleTargets)
        {
            float distance = Vector3.Distance(pt.transform.position, transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = pt;
            }
        }

        Debug.Log("Target is: " + (target != null ? target.name : "None"));
        return target;
    }
}