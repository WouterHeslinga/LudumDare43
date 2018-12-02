using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public Sprite interfaceSprite;
    public abstract int BoneCost { get; }
    public abstract int MeatCost { get; }

    protected virtual void Start()
    {
        FindObjectOfType<BuildingManager>().RegisterBuilding(this);
    }
}
