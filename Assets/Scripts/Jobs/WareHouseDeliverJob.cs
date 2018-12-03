using UnityEngine;

public class WareHouseDeliverJob : Job
{
    private ICollectable collectable;
    public override string Description => "Delivering";

    public WareHouseDeliverJob(ICollectable collectable, Vector2 location) : base(location, 1, null)
    {
        this.collectable = collectable;

        WareHouse wareHouse = BuildingManager.Instance.GetWareHouse();
        Location = wareHouse.GetRandomSpace();

        OnJobCompleted += () => { Owner.Collectable = null; };
        OnJobCompleted += () => { wareHouse.DeliverResource(Location, collectable as Resource); };
    }

    public override void CancelJob()
    {
        JobQueue.Enqueue(new CollectJob(collectable, collectable.Transform.position));
    }
}
