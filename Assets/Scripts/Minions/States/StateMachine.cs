using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StateMachine
{
    private Minion owner;
    private IMinionState currentState;
    public MinionStatus Status => currentState.Status;

    public StateMachine(Minion owner, IMinionState startingState)
    {
        this.owner = owner;
        ChangeState(startingState);
    }

    public void ChangeState(IMinionState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(owner);

        //If we get a new state given to us we should drop what we are doing and enter the new state
        if (owner.CurrentJob != null && newState.Status == MinionStatus.Reproducing)
            owner.CurrentJob.CancelJob();
    }

    public void UpdateCurrentState()
    {
        currentState.Update();
    }
}

