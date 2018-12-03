using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCorpse : MonoBehaviour, ICollectable
{
    public Transform Transform => transform;
    public bool IsActive
    {
        get
        {
            return GetComponent<PolygonCollider2D>().enabled;
        }
        set
        {
            GetComponent<PolygonCollider2D>().enabled = value;
        }
    }

    public void Collect()
    {
        if (FindObjectOfType<BuildingManager>().GetButchery() == null)
            return; //TODO: feed back?

        FindObjectOfType<JobQueue>().Enqueue(new ButcherCollectJob(this, transform.position));
    }
}
