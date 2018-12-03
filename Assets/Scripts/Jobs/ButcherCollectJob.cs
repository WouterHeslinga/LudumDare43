using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ButcherCollectJob : Job
{
    public ICollectable corpse;
    public override string Description => "Corpse Collection";
    private Butchery butchery;

    public ButcherCollectJob(ICollectable corpse, Vector2 location) : base(location, 1, null)
    {
        this.corpse = corpse;

        butchery = BuildingManager.Instance.GetButchery();
        Location = location;

        OnJobCompleted += () => { Owner.Collectable = corpse; };
        OnJobCompleted += () => { NewDeliveryJob(); };
    }

    private void NewDeliveryJob()
    {
        var newJob = new ButcherDeliveryJob(butchery, butchery.transform.position)
        {
            Owner = Owner,
            JobQueue = JobQueue,
            collectable = corpse
        };

        Owner.CurrentJob = newJob;
    }
}
