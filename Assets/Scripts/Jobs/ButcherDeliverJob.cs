using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ButcherDeliveryJob : Job
{
    public ICollectable collectable;
    public override string Description => "Delivering";
    private Butchery butchery;

    public ButcherDeliveryJob(Butchery butchery, Vector2 location) : base(location, 1, null)
    {
        this.butchery = butchery;
        Location = butchery.RandomSpot;

        OnJobCompleted += () => { butchery.Deliver(Location, collectable); };
        OnJobCompleted += () => { Owner.Collectable = null; };
    }

    public override void CancelJob()
    {
        JobQueue.Enqueue(new ButcherCollectJob(collectable, collectable.Transform.position));
    }
}
