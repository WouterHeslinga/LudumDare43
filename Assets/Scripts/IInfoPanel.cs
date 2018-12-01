using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IInfoPanel
{
    GameObject GameObject { get; }
    Transform Transform { get; }

    void UpdateInfo(IStats stats);
}
