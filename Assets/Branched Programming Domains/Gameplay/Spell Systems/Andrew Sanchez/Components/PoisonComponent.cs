using Unity.Entities;

public struct PoisonComponent : IComponentData
{
    public float poisonTimer;
    public float poisonDamage;
}
