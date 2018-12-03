using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ButcherJob : Job
{
    public ButcherJob(Vector2 location, JobCompleted jobCompletion = null) : base(location, 5, jobCompletion)
    {
        Location = location;        

        OnJobCompleted += () => { Owner.CurrentJob = null; };
        OnJobCompleted += () => { Owner.Collectable = null; };
    }

    public override void DoWork()
    {
        base.DoWork();
        Owner.Animator.SetBool("IsButchering", true);
        Owner.Animator.SetBool("IsRunning", false);
    }
}
