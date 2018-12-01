using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Resource : MonoBehaviour, ICollectable
{
    public Transform Transform => transform;

    private ResourceType type;

    public void Collect()
    {
        FindObjectOfType<JobQueue>().Enqueue(new CollectJob(this, transform.position));
    }

    public void Initialize(ResourceType type)
    {
        this.type = type;
    }
}
