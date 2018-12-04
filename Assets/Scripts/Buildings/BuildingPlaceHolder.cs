using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlaceHolder : Building
{
    public override int BoneCost { get; set; } = 0;
    public override int MeatCost { get; set; } = 0;
    public Building buildingToBuild;
    public override string TooltipText => $"";
    private JobQueue jobQueue;

    private List<ICollectable> deliveredResources;
    private float cooldown = 60;

    // Start is called before the first frame update
    new private void Start()
    {
        deliveredResources = new List<ICollectable>();
        jobQueue = FindObjectOfType<JobQueue>();
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;

        if(!jobQueue.ConstructionJobsAvailable() && cooldown <= 0)
        {
            if (BoneCost < buildingToBuild.BoneCost)
                AskResources(ResourceType.Bones);

            if(MeatCost < buildingToBuild.MeatCost)
                AskResources(ResourceType.Meat);

            cooldown = 60;
        }
    }

    public void Initialize(Building buildingToBuild)
    {
        this.buildingToBuild = buildingToBuild;

        for (int i = 0; i < buildingToBuild.BoneCost; i++)
        {
            AskResources(ResourceType.Bones);
        }

        for (int i = 0; i < buildingToBuild.MeatCost; i++)
        {
            AskResources(ResourceType.Meat);
        }
    }

    public void AddResource(ICollectable resource)
    {
        var res = resource as Resource;
        res.IsActive = false;

        if (res.type == ResourceType.Bones)
            BoneCost++;
        else MeatCost++;

        deliveredResources.Add(resource);

        if(BoneCost == buildingToBuild.BoneCost && MeatCost == buildingToBuild.MeatCost)
        {
            FinishBuilding();
        }
    }

    private void FinishBuilding()
    {
        foreach (var item in deliveredResources)
        {
            Destroy(item.Transform.gameObject);
        }

        var pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        var newBuilding = Instantiate(buildingToBuild, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void AskResources(ResourceType type)
    {
        var wareHouse = BuildingManager.Instance.GetWareHouse();
        var spot = wareHouse.GetRandomSpace();
        var res = wareHouse.GetResource(type);

        FindObjectOfType<JobQueue>().Enqueue(new ConstructionCollectJob(this, res, spot));
    }
}
