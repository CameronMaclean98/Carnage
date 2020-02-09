using Unity.Entities;

public struct ManaRegeneration : IComponentData
{
    public float Time;
    public float Reset;
    public float Max;
    public float Value;
}
