using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStats : IStats
{
    public MinionStats(Minion owner, int age = -1)
    {
        this.owner = owner;
        Gender = (Gender)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Gender)).Length);
        Age = age == -1 ? UnityEngine.Random.Range(18, 40) : age;
        Health = 100;
        Hunger = 100;
        Sanity = 100;
        MaxSpeed = 1f;
        ReproduceTimer = UnityEngine.Random.Range(30, 60);
    }

    public StatChanged OnStatChanged { get; set; }

    private Minion owner;
    public Gender Gender { get; private set; }
    public int Age { get; private set; }
    public int Health { get; private set; }
    public int Hunger { get; set; }
    public int Sanity { get; private set; }
    public float MaxSpeed { get; private set; }
    public string Status => owner.stateMachine.Status.ToString();

    public bool IsHungry => Hunger <= 60;
    public bool IsSane => Sanity > 50;
    public bool CanReproduce => Age >= 20 && ReproduceTimer <= 0;
    public float Speed => MaxSpeed;

    private float ageTimer = 15;
    public float ReproduceTimer { get; set; }
    public bool CanWork => Age >= 12 && !IsHungry;

    public void UpdateStats()
    {
        Hunger--;
        Sanity--;

        ReproduceTimer--;
        ageTimer--;

        if (ageTimer <= 0)
        {
            Age++;
            ageTimer = 30;
        }

        OnStatChanged?.Invoke(this);
    }

    public IMinionState GetNeeds()
    {
        if (CanReproduce && !IsHungry && IsSane)
            return new ReproductionState();

        return null;
    }
}
