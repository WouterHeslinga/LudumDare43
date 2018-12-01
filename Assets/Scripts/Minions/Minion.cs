using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour, IHasInfoPanel
{
    public Transform Transform => transform;

    public StateMachine stateMachine { get; private set; }
    public MinionStats stats { get; private set; }

    public Job CurrentJob { get; private set; }

    //TODO: Find a way to fix this so this class only has 1 Stats class but can use the MinionStats here while also implementing the IHasInfoPanel interface
    public IStats Stats => stats;

    private void Start()
    {
        stats = new MinionStats(this);
        stateMachine = new StateMachine(this, new IdleState());

        InvokeRepeating(nameof(UpdateStats), 1, 1);
    }

    private void Update()
    {
        stateMachine.UpdateCurrentState();        
    }

    public void CheckForNewJob()
    {
        var jobQueue = FindObjectOfType<JobQueue>();

        if (jobQueue.JobsAvailable)
        {
            CurrentJob = jobQueue.DeQueue();
            CurrentJob.OnJobCompleted += () => CurrentJob = null;

            stateMachine.ChangeState(new WorkingState());
        }
        else
            stateMachine.ChangeState(new IdleState());
    }

    private void UpdateStats()
    {
        stats.UpdateStats();
    }
}
