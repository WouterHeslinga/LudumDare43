using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFactory : MonoBehaviour
{
    private static Dictionary<ResourceType, Sprite> resourceSprites;

    private void Start()
    {
        resourceSprites = new Dictionary<ResourceType, Sprite>()
        {
            { ResourceType.Bones, Resources.Load<Sprite>("Sprites/Bone") },
            { ResourceType.Meat, Resources.Load<Sprite>("Sprites/Meat") },
            { ResourceType.Food, Resources.Load<Sprite>("Sprites/Food") }
        };
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            CreateResource(ResourceType.Bones, Vector2.zero);
        if (Input.GetKeyDown(KeyCode.S))
            CreateResource(ResourceType.Meat, Vector2.zero);
        if (Input.GetKeyDown(KeyCode.D))
            CreateResource(ResourceType.Food, Vector2.zero);
    }

    public static void CreateResource(ResourceType type, Vector2 position)
    {
        GameObject go = new GameObject();
        go.AddComponent<SpriteRenderer>().sprite = resourceSprites[type];
        go.AddComponent<Resource>().Initialize(type);
        go.AddComponent<BoxCollider2D>();
        go.name = $"{type}";

        go.transform.position = position;
    }
}
