using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStats
{
    public MinionStats()
    {
        Gender = (Gender)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Gender)).Length +1);
        Age = UnityEngine.Random.Range(18,40);
        Health = 100;
        Hunger = 100;
        Sanity = 100;
        MaxSpeed = 1f;
    }

    public delegate void StatChanged(MinionStats stats);
    public StatChanged OnStatChanged;

    public Gender Gender { get; private set; }
    public int Age { get; private set; }
    private float ageTimer = 0;
    public int Health { get; private set; }
    public int Hunger { get; set; }
    public int Sanity { get; private set; }
    public float MaxSpeed { get; private set; }

    public bool IsHungry => Hunger <= 60;
    public bool IsSane => Sanity > 50;
    public bool CanReproduce => Age >= 20;
    public float Speed => IsHungry ? MaxSpeed / 2 : MaxSpeed;

    public void Update()
    {
        Hunger--;
        Sanity--;

        ageTimer++;
        if (ageTimer >= 10)
        {
            Age++;
            ageTimer = 0;
        }

        OnStatChanged?.Invoke(this);
    }

    public void Log()
    {
        Debug.Log("Age " + Age);
        Debug.Log("Hunger " + Hunger);
        Debug.Log("Speed " + Speed);
        Debug.Log("Reproduce " + CanReproduce);
    }
}
