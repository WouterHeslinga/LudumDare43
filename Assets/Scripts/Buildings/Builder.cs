using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private Building selectedBuilding;
    private GameObject buildingGhost;

    public GameObject[] buildingPrefabs;

    private Color Red => Color.red;
    private Color Green => Color.green;
    private SpriteRenderer spriteRend;
    private Vector3 rotation = new Vector3();

    public void SelectBuilding(int index)
    {
        selectedBuilding = buildingPrefabs[index].GetComponent<Building>();

        buildingGhost = new GameObject("BuildingGhost");
        spriteRend = buildingGhost.AddComponent<SpriteRenderer>();
        spriteRend.sprite = selectedBuilding.GetComponent<SpriteRenderer>().sprite;

        var color = spriteRend.color;
        color.a = .5f;
        spriteRend.color = color;
    }

    private void Update()
    {
        if(buildingGhost != null)
        {
            Vector3 pos = FollowMouse();

            if (Input.GetKeyDown(KeyCode.R))
            {
                rotation += new Vector3(0, 0, -90);
                buildingGhost.transform.Rotate(0, 0, -90);
            }

            if (IsHoveringOverOtherBuilding(pos))
            {
                spriteRend.color = Red;
            }
            else
            {
                //TODO: Check if we have enough resources to build this building
                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(buildingGhost);
                    buildingGhost = null;

                    Instantiate(selectedBuilding, pos, Quaternion.Euler(rotation));
                    rotation = Vector3.zero;
                }

                spriteRend.color = Green;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(buildingGhost);
                buildingGhost = null;
            }
        }
    }

    private bool IsHoveringOverOtherBuilding(Vector3 pos)
    {
        var objects = Physics2D.BoxCastAll(pos, buildingGhost.GetComponent<SpriteRenderer>().sprite.bounds.size * 1.5f, 0, Vector2.zero);

        if (objects.Length == 0)
            return false;

        foreach (var obj in objects)
        {
            if (obj.transform.GetComponent<Building>() != null)
                return true;
        }

        return false;
    }

    private Vector3 FollowMouse()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        buildingGhost.transform.position = pos;
        return pos;
    }
}
