using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IMinionState
{
    Minion Owner { get; }
    MinionStatus Status { get; }

    void Enter(Minion owner);
    void Update();
    void Exit();
}

