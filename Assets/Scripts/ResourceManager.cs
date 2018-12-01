using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public delegate void ResourcesChanged();
    public ResourcesChanged OnResourcesChanged;

    public Dictionary<ResourceType, int> Resources { get; private set; }

    private void Start()
    {
        Resources = new Dictionary<ResourceType, int>()
        {
            {ResourceType.Bones, 5 },
            {ResourceType.Food, 100 },
            {ResourceType.Meat, 5 }
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
            Resources[type] += amount;

        OnResourcesChanged?.Invoke();
        //TODO: error handling, show message on screen??
    }

    private bool EnoughResources(ResourceType type, int amount)
    {
        return Resources[type] >= amount;
    } 
}
