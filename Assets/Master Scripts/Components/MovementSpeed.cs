using System;
using Unity.Entities;

[Serializable]
public struct MovementSpeed : IComponentData
{
    public float Max;
    public float Value;
}