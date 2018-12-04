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

    public bool ConstructionJobsAvailable()
    {
        foreach (var job in jobs)
        {
            if (job.GetType() == typeof(ConstructionCollectJob) || job.GetType() == typeof(ConstructionDeliverJob))
                return true;
        }
        return false;
    }

    public void Enqueue(Job job)
    {
        Debug.Log("Enqueue " + job.GetType());

        jobs.Add(job);
    }

    public Job Dequeue(Minion owner)
    {
        if (!JobsAvailable)
            return null;

        Job newJob = jobs[0];
        Debug.Log("Took " + newJob.GetType());
        newJob.JobQueue = this;
        newJob.Owner = owner;

        jobs.RemoveAt(0);

        return newJob;
    }
}

