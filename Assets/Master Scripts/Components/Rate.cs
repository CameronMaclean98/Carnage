using Unity.Entities;

public struct Rate : IComponentData
{
    public float Time;
    public float Cooldown;
    public float Max;
    public float Value;
}
