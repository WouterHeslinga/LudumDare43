using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : Building
{
    public override int BoneCost => 50;
    public override int MeatCost => 25;

    public Transform prayingSpot;
}
