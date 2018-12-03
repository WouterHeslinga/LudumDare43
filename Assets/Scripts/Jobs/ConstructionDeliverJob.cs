﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ConstructionDeliverJob : Job
{
    public ICollectable collectable;
    public override string Description => "Constructing";
    private BuildingPlaceHolder construction;

    public ConstructionDeliverJob(BuildingPlaceHolder construction, Vector2 location) : base(location, 1, null)
    {
        this.construction = construction;
        Location = location;

        OnJobCompleted += () => { construction.AddResource(collectable); };
        OnJobCompleted += () => { Owner.Collectable = null; };
    }

    public override void CancelJob()
    {
        var newJob = new ConstructionCollectJob(construction, (Resource)collectable, collectable.Transform.position);
        JobQueue.Enqueue(newJob);
    }
}