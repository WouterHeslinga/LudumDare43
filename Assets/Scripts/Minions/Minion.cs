using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour, IHasInfoPanel
{
    public Transform Transform => transform;

    public StateMachine stateMachine { get; private set; }
    public MinionStats stats { get; private set; }

    public Job CurrentJob { get; set; }
    public ICollectable Collectable { get; set; }
    public Animator Animator { get; private set; }

    //TODO: Find a way to fix this so this class only has 1 Stats class but can use the MinionStats here while also implementing the IHasInfoPanel interface
    public IStats Stats => stats;

    private void Start()
    {
        stats = new MinionStats(this);
        stateMachine = new StateMachine(this, new IdleState());
        Animator = GetComponent<Animator>();

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
            CurrentJob = jobQueue.Dequeue(this);

            //When the job doesnt have any oncompletion methods we can assume we dont get a follow up job, so we can set our job to null.
            if(CurrentJob.OnJobCompleted.GetInvocationList().Length == 0)
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

    public void Kill()
    {
        ResourceFactory.CreateResource(1, 3, ResourceType.Bones, transform.position);
        ResourceFactory.CreateResource(1, 3, ResourceType.Meat, transform.position);

        CurrentJob?.CancelJob();

        Destroy(gameObject);
    }
}
