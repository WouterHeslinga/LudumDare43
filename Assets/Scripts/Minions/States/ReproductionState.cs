﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductionState : IMinionState
{
    public Minion Owner { get; private set; }
    public MinionStatus Status => MinionStatus.Reproducing;

    private List<Minion> validPartners;
    private Minion chosenPartner;
    private float reproductionDuration = 5;
    private MinionManager minionManager;

    public ReproductionState(Minion partner = null)
    {
        chosenPartner = partner;
    }

    public void Enter(Minion owner)
    {
        this.Owner = owner;
        minionManager = MinionManager.Instance;

        if (chosenPartner == null)
        {
            validPartners = minionManager.FindAvailabePartners(Owner);

            //There aren't any valid partners add a cooldown to reproduction so we dont stay in an infinite loop to look for partners
            if (validPartners.Count == 0)
            {
                Owner.stats.ReproduceTimer = 2;
                Owner.CheckForNewJob();
            }
            else
            {
                //Pick a partner and force that partner to reproduce
                chosenPartner = validPartners[Random.Range(0, validPartners.Count)];
                chosenPartner.stateMachine.ChangeState(new ReproductionState(Owner));
            }
        }
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        //Something happenend with our partner?
        if (chosenPartner == null || !minionManager.HaveMinionSpace || Owner == null)
        {
            Owner.CheckForNewJob();
            chosenPartner.CheckForNewJob();
        }           

        if (Vector2.Distance(Owner.transform.position, chosenPartner.transform.position) > .5f)
            MoveTowardsDestination();
        else
            ReproduceWithPartner();
    }

    private void MoveTowardsDestination()
    {
        Owner.Animator.SetBool("IsMoving", true);
        LookAtDestination();
        Owner.transform.position = Vector2.MoveTowards(Owner.transform.position, chosenPartner.transform.position, Owner.stats.CurrentSpeed * Time.deltaTime);
    }

    private void LookAtDestination()
    {
        Vector2 direction = chosenPartner.transform.position - Owner.transform.position;
        direction.Normalize();

        float zRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Owner.transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
    }

    private void ReproduceWithPartner()
    {
        reproductionDuration -= Time.deltaTime;
        if(reproductionDuration <= 0)
        {
            minionManager.CreateNewMinion(0, Owner.transform.position);
            Owner.stats.ReproduceTimer = Random.Range(20, 40);
            Owner.CheckForNewJob();
        }
    }
}
