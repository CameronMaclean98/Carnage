using Unity.Entities;

public struct OrcAIUnitSpawnerSingleton : IComponentData
{
    // The 7 types of Orcs
    public int Brutes;
    public int Sluggers;
    public int Warlords;
    public int Raiders;
    public int Warlocks;
    public int Demolishers;
    public int Wyverns;

    // Total time of gameplay
    public float gameTime;

    // Counter for spawner
    public float spawntime;

    // Multiplier for count of units spawned each minute
    public int multiplier;

    // Counter to determine to load spawner counts
    public float loadSpawner;

};
