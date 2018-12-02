using UnityEngine;

public class DeliverJob : Job
{
    private ICollectable collectable;
    public override string Description => "Delivering";

    public DeliverJob(ICollectable collectable, Vector2 location) : base(location, 1, null)
    {
        Location = location;
        this.collectable = collectable;

        OnJobCompleted += () => { Owner.Collectable = null; };
    }

    public override void CancelJob()
    {
        JobQueue.Enqueue(new CollectJob(collectable, collectable.Transform.position));
    }
}
