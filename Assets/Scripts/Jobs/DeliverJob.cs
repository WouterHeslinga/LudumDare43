using UnityEngine;

public class DeliverJob : Job
{
    private ICollectable collectable;

    public DeliverJob(ICollectable collectable, Vector2 location, JobCompleted jobCompletion = null) : base(location, 1, jobCompletion)
    {
        Location = location;
        this.collectable = collectable;

        if (jobCompletion != null)
            OnJobCompleted += jobCompletion;
    }
}
