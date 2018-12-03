using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductionState : IMinionState
{
    public Minion Owner { get; private set; }
    public MinionStatus Status => MinionStatus.Reproducing;

    private List<Minion> validPartners;
    private Minion chosenPartner;

    public ReproductionState(Minion partner = null)
    {
        chosenPartner = partner;
    }

    public void Enter(Minion owner)
    {
        this.Owner = owner;

        if(chosenPartner == null)
        {
            validPartners = MinionManager.Instance.FindAvailabePartners(Owner);

            //There aren't any valid partners add a cooldown to reproduction so we dont stay in an infinite loop to look for partners
            if (validPartners.Count == 0)
            {
                Owner.stats.ReproduceTimer = 5;
                Owner.CheckForNewJob();
            }
            else
            {
                //Pick a partner and force that partner to reproduce
                chosenPartner = validPartners[Random.Range(0, validPartners.Count - 1)];
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
        if (chosenPartner == null)
            Owner.CheckForNewJob();

        if (Vector2.Distance(Owner.transform.position, chosenPartner.transform.position) > .5f)
            MoveTowardsDestination();
        else
            ReproduceWithPartner();
    }

    private void MoveTowardsDestination()
    {
        Owner.Animator.SetBool("IsMoving", true);
        LookAtDestination();
        Owner.transform.position = Vector2.MoveTowards(Owner.transform.position, chosenPartner.transform.position, Owner.stats.Speed * Time.deltaTime);
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
        MinionManager.Instance.CreateNewMinion(0, Owner.transform.position);
        Owner.stats.ReproduceTimer = Random.Range(15, 30);
        Owner.CheckForNewJob();
    }
}
