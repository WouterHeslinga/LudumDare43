using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour, IHasInfoPanel
{
    [SerializeField] private GameObject corpsePrefab;

    public Transform Transform => transform;

    public StateMachine stateMachine { get; private set; }
    public MinionStats stats { get; private set; }

    public Job CurrentJob { get; set; }
    public ICollectable Collectable { get; set; }
    public Animator Animator { get; private set; }

    //TODO: Find a way to fix this so this class only has 1 Stats class but can use the MinionStats here while also implementing the IHasInfoPanel interface
    public IStats Stats => stats;

    public void Initialize(MinionStats stats)
    {
        this.stats = stats;
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
        if (!stats.CanWork)
            stateMachine.ChangeState(new IdleState());

        //Check if our stats need work.
        if (stats.GetNeeds() != null)
        {
            var newState = stats.GetNeeds();
            stateMachine.ChangeState(newState);
            return;
        }

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

    public void Die(bool spawnCorpse)
    {
        if (spawnCorpse)
            Instantiate(corpsePrefab, transform.position, transform.rotation);

        MinionManager.Instance.RemoveMinion(this);

        CurrentJob?.CancelJob();

        Destroy(gameObject);
    }
}
