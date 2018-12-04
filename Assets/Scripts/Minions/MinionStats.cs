using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStats : IStats
{
    public MinionStats(Minion owner, Gender gender, int age = -1)
    {
        this.owner = owner;
        Gender = gender == Gender.Random ? (Gender)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Gender)).Length - 1) : gender;
        Age = age == -1 ? UnityEngine.Random.Range(18, 35) : age;
        Health = 100;
        Hunger = 100;
        Sanity = 100;
        MaxSpeed = 2;
        ReproduceTimer = UnityEngine.Random.Range(20, 40);
    }

    public StatChanged OnStatChanged { get; set; }

    private Minion owner;
    public Gender Gender { get; private set; }
    public int Age { get; private set; }
    public int Health { get; private set; }
    public int Hunger { get; set; }
    public int Sanity { get; set; }
    public float MaxSpeed { get; set; }
    public string Status => owner.stateMachine.Status.ToString();

    public bool IsHungry => Hunger <= 50;
    public bool CanReproduce => Age >= 16 && ReproduceTimer <= 0;
    public float CurrentSpeed => (1f- ((float)Age / 125f)) * MaxSpeed;
    public float EatingCooldown = 0;

    private float ageTimer = 3;
    public float ReproduceTimer { get; set; }
    public bool CanWork => Age >= 12 && !IsHungry;

    public void UpdateStats()
    {
        Hunger--;

        ReproduceTimer--;
        ageTimer--;
        EatingCooldown--;

        if (ageTimer <= 0)
        {
            Age++;
            ageTimer = 3;

            if(Age > 70)
            {
                var diff = Age - 70;
                var percentage = 4 * diff;

                if (UnityEngine.Random.Range(0, 100) < percentage)
                    owner.Die(false);
            }
        }

        if (Hunger <= 0)
        {
            owner.Die(false);
        }

        OnStatChanged?.Invoke(this);
    }

    public IMinionState GetNeeds()
    {
        if (Sanity <= 0)
            return new InsaneState();

        if (IsHungry && EatingCooldown <= 0)
            return new EatingState();

        if (CanReproduce && !IsHungry && MinionManager.Instance.HaveMinionSpace)
            return new ReproductionState();

        return null;
    }
}
