using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butchery : Building
{
    public override int BoneCost => 15;
    public override int MeatCost => 5;

    public Transform butcheringSpot;
}
