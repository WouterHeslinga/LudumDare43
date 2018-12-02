using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouse : Building
{
    public override int BoneCost => 10;
    public override int MeatCost => 10;

    public Transform[] wareHouseSpots;
}
