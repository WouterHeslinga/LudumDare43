using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : Building
{
    public override int BoneCost { get; set; } = 50;
    public override int MeatCost { get; set; } = 25;
    public override string TooltipText => $"Bones: {BoneCost}\nMeat: {MeatCost}\nMake minions worship you and win the game!";

    public Transform prayingSpot;
}
