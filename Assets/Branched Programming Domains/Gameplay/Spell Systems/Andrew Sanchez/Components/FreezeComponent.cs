using Unity.Entities;

public struct FreezeComponent : IComponentData
{
    public float freezeTimer;
    public float originalSpeed;
}
