using Unity.Entities;

public enum AttackType
{
    Piercing,
    Slashing,
    Bludgeoning,
    Magical,
    Artillery
}

public struct Damage : IComponentData
{
    public AttackType Type;
    public float Min;
    public float Max;
}
