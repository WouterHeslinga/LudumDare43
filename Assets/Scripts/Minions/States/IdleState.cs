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
        if (destination == Vector2.zero)
            destination = (Vector2)Owner.transform.position + Random.insideUnitCircle * 5;

        //Are we in range to do work?
        if (Vector2.Distance(destination, Owner.transform.position) > 0.5)
            MoveTowardsDestination();
        else
        {
            destination = Vector2.zero;
            Owner.CheckForNewJob();
        }
    }

    private void MoveTowardsDestination()
    {
        Owner.Animator.SetBool("IsMoving", true);
        LookAtDestination();
        Owner.transform.position = Vector2.MoveTowards(Owner.transform.position, destination, Owner.stats.Speed * Time.deltaTime);
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
