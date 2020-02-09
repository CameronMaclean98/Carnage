using Unity.Entities;

public enum ArmorType
{
    None,
    Light,
    Medium,
    Heavy,
    Fortified
}

public struct Health : IComponentData
{
    public ArmorType Type;
    public int Armor;
    public float Max;
    public float Value;
}
