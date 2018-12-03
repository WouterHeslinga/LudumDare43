using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Butchery : Building
{
    public override int BoneCost { get; set; } = 1;
    public override int MeatCost { get; set; } = 1;
    public bool Busy { get; private set; }

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

        ResourceFactory.CreateResource(2, 5, ResourceType.Food, resourceOutput.position);
        ResourceFactory.CreateResource(1, 3, ResourceType.Bones, resourceOutput.position);

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
