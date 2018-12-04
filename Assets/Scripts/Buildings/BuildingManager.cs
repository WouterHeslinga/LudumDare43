using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private List<Building> allBuildings;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        allBuildings = new List<Building>();
    }

    public void RegisterBuilding(Building building)
    {
        allBuildings.Add(building);
    }

    public WareHouse GetWareHouse()
    {
        foreach (Building building in allBuildings)
        {
            if (building.GetType() != typeof(WareHouse))
                continue;

            return building as WareHouse;
        }

        return null;
    }

    public Butchery GetButchery()
    {
        List<Butchery> butcheries = new List<Butchery>();
        foreach (Building building in allBuildings)
        {
            if (building.GetType() != typeof(Butchery))
                continue;
            butcheries.Add(building as Butchery);
        }

        if (butcheries.Count > 0)
            return butcheries[Random.Range(0, butcheries.Count)];
        else
            return null;
    }

    public float furthestBuidling(Minion minion)
    {
        float distance = 0;
        foreach (var item in allBuildings)
        {
            if (distance < Vector2.Distance(item.transform.position, minion.transform.position))
                distance = Vector2.Distance(item.transform.position, minion.transform.position);
        }
        return distance;
    }
}
