using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WorkingState : IMinionState
{
    public Minion Owner { get; private set; }
    public string Status => "Work Work";

    public void Enter(Minion owner)
    {
        this.Owner = owner;
    }

    public void Exit()
    {
    }

    public void Update()
    {
        //Check if we have a job
        if (Owner.CurrentJob != null)
        {
            //Are we in range to do work?
            if (Vector2.Distance(Owner.CurrentJob.Location, Owner.transform.position) > 0.5)
                Owner.transform.position = Vector2.MoveTowards(Owner.transform.position, Owner.CurrentJob.Location, Owner.stats.Speed * Time.deltaTime);
            else
                Owner.CurrentJob.DoWork();
        }
        else
            Owner.CheckForNewJob();
    }
}
