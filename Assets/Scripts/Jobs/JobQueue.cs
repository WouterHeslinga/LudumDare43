using UnityEngine;
using System.Collections.Generic;

public class JobQueue : MonoBehaviour
{
    private List<Job> jobs;

    private void Start()
    {
        jobs = new List<Job>();
    }

    public bool JobsAvailable => jobs.Count > 0;

    public void Enqueue(Job job)
    {
        jobs.Add(job);
    }

    public Job DeQueue(Minion owner)
    {
        if (!JobsAvailable)
            return null;

        Job newJob = jobs[0];
        jobs.RemoveAt(0);
        newJob.Owner = owner;
        return newJob;
    }
}

