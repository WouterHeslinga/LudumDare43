using UnityEngine;

public interface IHasInfoPanel
{
    Transform Transform { get; }
    IStats Stats { get; }
}