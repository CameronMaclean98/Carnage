using Unity.Entities;
using Unity.Mathematics;

public enum UnitAction
{
    Defend,
    Attack,
    Patrol,
    Hold,
    Move,
    Retreat
}

public struct Target : IComponentData
{
    public Entity Entity;
    public float3 Destination;
    public UnitAction Action;
}
