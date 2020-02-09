using Unity.Entities;

public struct HealthRegeneration : IComponentData
{
    public float Time; // Current Time of the regeneration timer
    public float Reset; // Resets the Time if Time exceeds cooldown threshold
    public float Max; // Maximum value the health of the unit can regenerate
    public float Value; // The current Value the health of the unit regenerates
}
