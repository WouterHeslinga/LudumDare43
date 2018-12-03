using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ConstructionCollectJob : Job
{
    public ICollectable collectable;
    public override string Description => "Collecting Constructing material";
    private BuildingPlaceHolder construction;

    public ConstructionCollectJob(BuildingPlaceHolder construction, Resource res, Vector2 location) : base(location, 1, null)
    {
        this.construction = construction;

        collectable = res;
        Location = location;

        OnJobCompleted += () => { Owner.Collectable = collectable; };
        OnJobCompleted += () => { NewDeliverJob(); };
    }

    private void NewDeliverJob()
    {
        var newJob = new ConstructionDeliverJob(construction, construction.transform.position)
        {
            Owner = Owner,
            JobQueue = JobQueue,
            collectable = collectable
        };

        Owner.CurrentJob = newJob;
    }

    public override void CancelJob()
    {
        //BuildingManager.Instance.GetWareHouse().DeliverResource(BuildingManager.Instance.GetWareHouse().GetRandomSpace(), (Resource)collectable);
        base.CancelJob();
    }
}
