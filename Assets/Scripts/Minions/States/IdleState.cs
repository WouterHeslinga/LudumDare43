using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class IdleState : IMinionState
{
    public Minion Owner { get; private set; }

    public void Enter(Minion owner)
    {
        this.Owner = owner;
    }
    
    public void Update()
    {
        if (Owner.ReachedDestination)
        {
            if(Owner.NewJobAvailable() == false)
                Owner.SetDestination((Vector2)Owner.transform.position + UnityEngine.Random.insideUnitCircle * 2);
        }
    }

    public void Exit()
    {
    }
}
