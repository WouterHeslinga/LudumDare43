using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private StateMachine stateMachine;
    public MinionStats minionStats { get; private set; }

    public Vector2 Destination;
    public bool ReachedDestination;

    private void Start()
    {
        minionStats = new MinionStats();
        stateMachine = new StateMachine(this, new IdleState());

        InvokeRepeating(nameof(UpdateStats), 1, 1);
    }

    private void Update()
    {
        stateMachine.UpdateCurrentState();

        if (Vector2.Distance(transform.position, Destination) > .5f)
            MoveTowardsDestination();
        else ReachedDestination = true;        
    }

    public bool NewJobAvailable()
    {
        return false;
    }

    private void MoveTowardsDestination()
    {
        transform.position = Vector2.MoveTowards(transform.position, Destination, minionStats.Speed * Time.deltaTime);
    }

    public void SetDestination(Vector2 destination)
    {
        ReachedDestination = false;
        Destination = destination;
    }

    private void UpdateStats()
    {
        minionStats.Update();
    }
}
