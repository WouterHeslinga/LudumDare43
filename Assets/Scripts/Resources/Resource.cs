﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Resource : MonoBehaviour, ICollectable
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
    public ResourceType type;

    public void Collect()
    {
        FindObjectOfType<JobQueue>().Enqueue(new CollectJob(this, transform.position));
    }

    public void Initialize(ResourceType type)
    {
        this.type = type;
    }
}
