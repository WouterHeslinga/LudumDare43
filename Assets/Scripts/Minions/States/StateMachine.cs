using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StateMachine
{
    private Minion owner;
    private IMinionState currentState;

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
    }

    public void UpdateCurrentState()
    {
        currentState.Update();
    }
}

