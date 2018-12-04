using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WareHouse : Building
{
    public override int BoneCost { get; set; } = 10;
    public override int MeatCost { get; set; } = 10;
    public int ResourceSpace;
    public override string TooltipText => $"Bones: {BoneCost}\nMeat: {MeatCost}\nStores resources";

    public List<Transform> wareHouseSpots;

    private List<KeyValuePair<int, Resource>> Inventory;

    protected override void Start()
    {
        base.Start();
        Inventory = new List<KeyValuePair<int, Resource>>();

        for (int i = 0; i < 10; i++)
        {
            var location = GetRandomSpace();
            var resource = ResourceFactory.CreateResource(ResourceType.Bones, location);
            DeliverResource(GetRandomSpace(), resource.GetComponent<Resource>());
        }

        for (int i = 0; i < 10; i++)
        {
            var location = GetRandomSpace();
            var resource = ResourceFactory.CreateResource(ResourceType.Meat, location);
            DeliverResource(GetRandomSpace(), resource.GetComponent<Resource>());
        }

        for (int i = 0; i < 10; i++)
        {
            var location = GetRandomSpace();
            var resource = ResourceFactory.CreateResource(ResourceType.Food, location);
            DeliverResource(GetRandomSpace(), resource.GetComponent<Resource>());
        }
    }

    public Vector2 GetRandomSpace()
    {
        return wareHouseSpots[Random.Range(0, wareHouseSpots.Count)].position;
    }

    public void Deliver(Vector2 deliverPosition, ICollectable collectable)
    {
        if(collectable.GetType() == typeof(Resource))
            DeliverResource(deliverPosition, collectable as Resource);

        else if (collectable.GetType() == typeof(ResourceCrate))
            DeliverCrate(collectable as ResourceCrate);
    }

    public void DeliverResource(Vector2 deliverPosition, Resource resource)
    {
        int index = wareHouseSpots.IndexOf(wareHouseSpots.First(x => (Vector2)x.transform.position == deliverPosition));
        resource.IsActive = false;
        Inventory.Add(new KeyValuePair<int, Resource>(index, resource));

        resource.transform.position = deliverPosition;

        FindObjectOfType<ResourceManager>().AddResources(resource.type, 1);
    }

    private void DeliverCrate(ResourceCrate crate)
    {
        foreach (var item in crate.resources)
        {
            for (int i = 0; i < item.Value; i++)
            {
                var spot = GetRandomSpace();
                var resource = ResourceFactory.CreateResource(item.Key, spot).GetComponent<Resource>();
                resource.IsActive = false;
                DeliverResource(spot, resource);
            }
        }
        Destroy(crate.gameObject);
    }

    public Resource GetResource(ResourceType type)
    {
        foreach (var item in Inventory)
        {
            if(item.Value.type == type && item.Value.IsActive == false)
            {
                item.Value.IsActive = true;
                Inventory.Remove(item);
                FindObjectOfType<ResourceManager>().RemoveResources(item.Value.type, 1);
                return item.Value;
            }
        }

        return null;
    }
}
