using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ButcherJob : Job
{
    private Butchery butcher;

    public ButcherJob(Vector2 location, Butchery butcher) : base(location, 5, null)
    {
        Location = location;
        this.butcher = butcher;

        OnJobCompleted += () => { this.butcher.CreateCrate(); };
        OnJobCompleted += () => { Owner.CurrentJob = null; };
        OnJobCompleted += () => { Owner.Collectable = null; };
    }

    public override void DoWork()
    {
        base.DoWork();
        Owner.Animator.SetBool("IsButchering", true);
        Owner.Animator.SetBool("IsMoving", false);
    }

    public override void CancelJob()
    {
        base.CancelJob();
        Owner.Animator.SetBool("IsButchering", false);
    }
}
