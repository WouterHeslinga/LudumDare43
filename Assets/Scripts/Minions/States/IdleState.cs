using UnityEngine;

public class IdleState : IMinionState
{
    public Minion Owner { get; private set; }
    public string Status => "Just running around";

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
            Owner.transform.position = Vector2.MoveTowards(Owner.transform.position, destination, Owner.stats.Speed * Time.deltaTime);
        else
        {
            destination = Vector2.zero;
            Owner.CheckForNewJob();
        }
    }

    public void Exit()
    {
    }
}
