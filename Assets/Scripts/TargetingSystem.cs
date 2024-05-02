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

        List<GameObject> posibleTargets = GameObject.FindGameObjectsWithTag(targetTag).ToList<GameObject>();

        float closetDistance = float.MaxValue;
        foreach(GameObject pt in posibleTargets)
        {
            float distance = Vector3.Distance(pt.transform.position, transform.position);

            if (distance < closetDistance)
            {
                closetDistance = distance;
                target = pt;
                
            }
            
        }
        Debug.Log("Target is: ", target);
        return target;
    }
}
