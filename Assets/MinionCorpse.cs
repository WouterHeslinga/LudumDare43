using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCorpse : MonoBehaviour, ICollectable
{
    public Transform Transform => transform;
    private bool isBeingCollected = false;
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
        {
            ErrorMessages.Instance.ShowMessage("Requires Butcher");
            return;
        }

        if (isBeingCollected)
        {
            ErrorMessages.Instance.ShowMessage("Already being collected");
            return;
        }

        isBeingCollected = true;
        FindObjectOfType<JobQueue>().Enqueue(new ButcherCollectJob(this, transform.position));
    }
}
