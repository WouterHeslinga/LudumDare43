using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Job
{
    public delegate void JobCompleted();
    public JobCompleted OnJobCompleted;

    /// <summary>
    /// Create a new job that can be executed by minions
    /// </summary>
    /// <param name="location">The location of the job</param>
    /// <param name="timeToComplete">How long it takes for minions to complete this task</param>
    /// <param name="jobCompletion">The method to run when the job is completed</param>
    public Job(Vector2 location, float timeToComplete, JobCompleted jobCompletion = null)
    {
        Location = location;
        TimeToComplete = timeToComplete;

        if(jobCompletion != null)
            OnJobCompleted += jobCompletion;
    }

    public Vector2 Location { get; private set; }
    public float TimeToComplete { get; private set; }

    public void DoWork()
    {
        TimeToComplete -= Time.deltaTime;

        if (TimeToComplete <= 0)
            OnJobCompleted?.Invoke();
    }
}

