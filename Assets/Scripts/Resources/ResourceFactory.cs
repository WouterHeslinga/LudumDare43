using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFactory : MonoBehaviour
{
    private static Dictionary<ResourceType, Sprite> resourceSprites;

    private void Awake()
    {
        resourceSprites = new Dictionary<ResourceType, Sprite>()
        {
            { ResourceType.Bones, Resources.Load<Sprite>("Sprites/Bone") },
            { ResourceType.Meat, Resources.Load<Sprite>("Sprites/Meat") },
            { ResourceType.Food, Resources.Load<Sprite>("Sprites/Food") }
        };
    }

    /// <summary>
    /// Create a resource object at the given position
    /// </summary>
    /// <param name="type">The type of resource</param>
    /// <param name="position">The position of the resource in world space</param>
    public static GameObject CreateResource(ResourceType type, Vector2 position)
    {
        GameObject go = new GameObject();
        go.AddComponent<SpriteRenderer>().sprite = resourceSprites[type];
        go.GetComponent<SpriteRenderer>().sortingOrder = 1;
        go.AddComponent<Resource>().Initialize(type);
        go.AddComponent<PolygonCollider2D>();
        go.name = $"{type}";

        go.transform.position = position;

        return go;
    }

    /// <summary>
    /// Create multiple resources at the given position
    /// </summary>
    /// <param name="minAmount">Min amount of resources to spawn</param>
    /// <param name="maxAmount">Max amount of resources to spawn</param>
    /// <param name="type">The type of resource</param>
    /// <param name="position">The position of the resource in world space</param>
    public static void CreateResource(int minAmount, int maxAmount, ResourceType type, Vector2 position)
    {
        for (int i = 0; i < Random.Range(minAmount, maxAmount + 1); i++)
        {
            var randomPos = position + Random.insideUnitCircle;
            CreateResource(type, randomPos);
        }
    }
}
