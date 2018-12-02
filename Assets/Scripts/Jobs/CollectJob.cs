using UnityEngine;

public class CollectJob : Job
{
    private ICollectable collectable;
    public override string Description => "Collecting";

    public CollectJob(ICollectable collectable, Vector2 location) : base(location, 1, null)
    {
        Location = location;
        this.collectable = collectable;

        OnJobCompleted += () => { Owner.Collectable = collectable; };
        OnJobCompleted += () => { NewDeliverJob(); };
    }

    private void NewDeliverJob()
    {
        var newJob = new DeliverJob(collectable, Vector2.zero)
        {
            Owner = Owner,
            JobQueue = JobQueue
        };

        Owner.CurrentJob = newJob;
    }
}
