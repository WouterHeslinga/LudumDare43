using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    private int populationIncrease = 10;

    public override int BoneCost => 10;
    public override int MeatCost => 10;

    protected override void Start()
    {
        base.Start();

        MinionManager.Instance.IncreaseMaxPopulation(populationIncrease);
    }
}
