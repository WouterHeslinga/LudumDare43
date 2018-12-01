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
                MoveTowardsDestination();
            else
                Owner.CurrentJob.DoWork();
        }
        else
            Owner.CheckForNewJob();

        if(Owner.Collectable != null)
        {
            Owner.Collectable.Transform.position = Owner.transform.position + Owner.transform.up;
        }
    }

    private void MoveTowardsDestination()
    {
        LookAtDestination();
        Owner.transform.position = Vector2.MoveTowards(Owner.transform.position, Owner.CurrentJob.Location, Owner.stats.Speed * Time.deltaTime);
    }

    private void LookAtDestination()
    {
        Vector2 direction = Owner.CurrentJob.Location - (Vector2)Owner.transform.position;
        direction.Normalize();

        float zRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Owner.transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
    }
}
