using UnityEngine;

public interface ICollectable
{
    Transform Transform { get; }
    bool IsActive { get; set; }

    void Collect();
}