using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    private int populationIncrease = 10;

    public override int BoneCost { get; set; } = 5;
    public override int MeatCost { get; set; } = 5;

    protected override void Start()
    {
        base.Start();

        MinionManager.Instance.IncreaseMaxPopulation(populationIncrease);
    }
}
