using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public delegate void ResourcesChanged();
    public ResourcesChanged OnResourcesChanged;

    public Dictionary<ResourceType, int> Resources { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Resources = new Dictionary<ResourceType, int>()
        {
            {ResourceType.Bones, 0 },
            {ResourceType.Food, 0 },
            {ResourceType.Meat, 0 }
        };
        OnResourcesChanged?.Invoke();
    }

    public void AddResources(ResourceType type, int amount)
    {
        Resources[type] += amount;
        OnResourcesChanged?.Invoke();
    }

    public void RemoveResources(ResourceType type, int amount)
    {
        if(EnoughResources(type, amount))
            Resources[type] -= amount;

        OnResourcesChanged?.Invoke();
    }

    public bool EnoughResources(ResourceType type, int amount)
    {
        return Resources[type] >= amount;
    }

    public bool EnoughResources(Building selectedBuilding)
    {
        if(EnoughResources(ResourceType.Bones, selectedBuilding.BoneCost) && EnoughResources(ResourceType.Meat, selectedBuilding.MeatCost))
            return true;
        else
        {
            ErrorMessages.Instance.ShowMessage("Not enough resources");
            return false;
        }
    }
}
