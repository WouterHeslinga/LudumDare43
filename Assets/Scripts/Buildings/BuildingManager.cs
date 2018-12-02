using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private List<Building> allBuildings;

    private void Awake()
    {
        allBuildings = new List<Building>();
    }

    public void RegisterBuilding(Building building)
    {
        allBuildings.Add(building);

        Debug.Log("Registered " + building.GetType());
    }
}
