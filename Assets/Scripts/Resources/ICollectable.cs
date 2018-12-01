using UnityEngine;

public interface ICollectable
{
    Transform Transform { get; }

    void Collect();
}