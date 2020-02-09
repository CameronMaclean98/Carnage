using Unity.Entities;
using Unity.Mathematics;

public struct AimSpellComponent : IComponentData
{
    public float timer;
    public float3 destination;
}
