using UnityEngine;

public class IdleState : IMinionState
{
    public Minion Owner { get; private set; }
    public MinionStatus Status => MinionStatus.Idle;

    private Vector2 destination = Vector2.zero;

    public void Enter(Minion owner)
    {
        this.Owner = owner;
    }
    
    public void Update()
    {
        if (Owner.Collectable == null && Owner.CurrentJob == null)
        {
            Owner.Animator.SetBool("HasCollectable", false);
            Owner.Animator.SetBool("IsButchering", false);
        }

        if (Owner.stats.GetNeeds() != null)
            Owner.CheckForNewJob();

        if (destination == Vector2.zero)
        {
            var furthestBuilding = BuildingManager.Instance.furthestBuidling(Owner);
            destination = Random.insideUnitCircle * furthestBuilding;
        }

        //Are we in range to do work?
        if (Vector2.Distance(destination, Owner.transform.position) > 0.5)
            MoveTowardsDestination();
        else
        {
            Owner.CheckForNewJob();
        }
    }

    private void MoveTowardsDestination()
    {
        Owner.Animator.SetBool("IsMoving", true);
        LookAtDestination();
        Owner.transform.position = Vector2.MoveTowards(Owner.transform.position, destination, Owner.stats.CurrentSpeed * Time.deltaTime);
    }

    private void LookAtDestination()
    {
        Vector2 direction = destination - (Vector2)Owner.transform.position;
        direction.Normalize();

        float zRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Owner.transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
    }

    public void Exit()
    {
    }
}
