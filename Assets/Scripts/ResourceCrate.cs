using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCrate : MonoBehaviour, ICollectable
{
    public Transform Transform => transform;
    public bool IsBeingCollected = false;
    public Dictionary<ResourceType, int> resources;

    public void Initialize(Dictionary<ResourceType, int> resources)
    {
        this.resources = resources;
    }

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
        if (FindObjectOfType<BuildingManager>().GetWareHouse() == null)
        {
            ErrorMessages.Instance.ShowMessage("Requires WareHouse");
            return;
        }

        if (IsBeingCollected)
        {
            ErrorMessages.Instance.ShowMessage("Already being collected");
            return;
        }

        IsBeingCollected = true;
        FindObjectOfType<JobQueue>().Enqueue(new CollectJob(this, transform.position));
    }
}
