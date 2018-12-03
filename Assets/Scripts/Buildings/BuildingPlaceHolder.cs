using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlaceHolder : Building
{
    public override int BoneCost { get; set; } = 0;
    public override int MeatCost { get; set; } = 0;
    public Building buildingToBuild;
    public override string TooltipText => $"";

    private List<ICollectable> deliveredResources;

    // Start is called before the first frame update
    new private void Start()
    {
        deliveredResources = new List<ICollectable>();
    }

    public void Initialize(Building buildingToBuild)
    {
        this.buildingToBuild = buildingToBuild;

        for (int i = 0; i < buildingToBuild.BoneCost; i++)
        {
            var wareHouse = BuildingManager.Instance.GetWareHouse();
            var spot = wareHouse.GetRandomSpace();
            var res = wareHouse.GetResource(ResourceType.Bones);

            FindObjectOfType<JobQueue>().Enqueue(new ConstructionCollectJob(this, res, spot));
        }

        for (int i = 0; i < buildingToBuild.MeatCost; i++)
        {
            var wareHouse = BuildingManager.Instance.GetWareHouse();
            var spot = wareHouse.GetRandomSpace();
            var res = wareHouse.GetResource(ResourceType.Meat);

            FindObjectOfType<JobQueue>().Enqueue(new ConstructionCollectJob(this, res, spot));
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
}
