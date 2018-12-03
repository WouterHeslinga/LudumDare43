using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public Sprite interfaceSprite;
    public abstract int BoneCost { get; set; }
    public abstract int MeatCost { get; set; }

    protected virtual void Start()
    {
        FindObjectOfType<BuildingManager>().RegisterBuilding(this);
    }
}
