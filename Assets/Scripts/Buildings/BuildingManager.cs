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

            if (((WareHouse)building).HasSpace)
                return building as WareHouse;
        }

        return null;
    }

    public Butchery GetButchery()
    {
        foreach (Building building in allBuildings)
        {
            if (building.GetType() != typeof(Butchery))
                continue;

            return building as Butchery;
        }

        return null;
    }
}
