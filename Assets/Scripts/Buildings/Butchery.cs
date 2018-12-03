using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Butchery : Building
{
    [SerializeField] private GameObject resourceCratePrefab;

    public override int BoneCost { get; set; } = 6;
    public override int MeatCost { get; set; } = 5;
    public bool Busy { get; private set; }
    public override string TooltipText => $"Bones: {BoneCost}\nMeat: {MeatCost}\nUsed by minions to butcher corpses for resources";
    public Transform butcheringSpot;
    public List<Transform> corpseSpots;
    public Transform resourceOutput;
    public List<KeyValuePair<int, ICollectable>> inventory;

    protected override void Start()
    {
        base.Start();
        inventory = new List<KeyValuePair<int, ICollectable>>();
    }

    private void Update()
    {
        if(!Busy && inventory.Count > 0)
        {
            Busy = true;
            FindObjectOfType<JobQueue>().Enqueue(new ButcherJob(butcheringSpot.position, () => { CompleteButcheringJob(); }));
        }
    }

    private void CompleteButcheringJob()
    {
        if (inventory.Count == 0)
            return;

        var crate = Instantiate(resourceCratePrefab, resourceOutput.transform.position, Quaternion.identity).GetComponent<ResourceCrate>();
        crate.Initialize(new Dictionary<ResourceType, int>()
        {
            { ResourceType.Bones, Random.Range(1,3) },
            { ResourceType.Meat, Random.Range(1,3) },
            { ResourceType.Food, Random.Range(5,10) },
        });

        Destroy(inventory[0].Value.Transform.gameObject);
        inventory.RemoveAt(0);

        Busy = false;
    }

    public Vector2 RandomSpot => corpseSpots[Random.Range(0, corpseSpots.Count)].position;

    public void Deliver(Vector2 deliverLocation, ICollectable corpse)
    {
        int index = corpseSpots.IndexOf(corpseSpots.First(x => (Vector2)x.transform.position == deliverLocation));
        corpse.IsActive = false;
        inventory.Add(new KeyValuePair<int, ICollectable>(index, corpse));

        corpse.Transform.position = deliverLocation;
    }
}
