using UnityEngine;

public class InsaneState : IMinionState
{
    public Minion Owner { get; private set; }

    public MinionStatus Status => MinionStatus.GoingInsane;
    private Minion target;

    public void Enter(Minion owner)
    {
        this.Owner = owner;

        Owner.GetComponent<SpriteRenderer>().color = Color.red;

        GetNewTarget();
    }

    public void Exit()
    {
        Owner.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Update()
    {
        if (target != null)
        {
            if (Vector2.Distance(Owner.transform.position, target.transform.position) > .5f)
            {
                MoveTowardsTarget();
            }
            else
                MurderTarget();
        }
        else
            GetNewTarget();
    }

    private void MurderTarget()
    {
        target.Die(true);
    }

    private void GetNewTarget()
    {
        target = MinionManager.Instance.GetTarget(Owner);
    }

    private void MoveTowardsTarget()
    {
        Owner.Animator.SetBool("IsMoving", true);
        LookAtDestination();
        Owner.transform.position = Vector2.MoveTowards(Owner.transform.position, target.transform.position, Owner.stats.CurrentSpeed * Time.deltaTime);
    }

    private void LookAtDestination()
    {
        Vector2 direction = target.transform.position - Owner.transform.position;
        direction.Normalize();

        float zRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Owner.transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
    }
}