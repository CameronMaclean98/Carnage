using Unity.Entities;

public struct HumanAIUnitSpawnerSingleton : IComponentData
{
    // The 7 types of Humans
    public int Knights;
    public int Paladins;
    public int SpellSlingers;
    public int Legionnaires;
    public int Marksmen;
    public int SiegeEngines;
    public int Griffins;

    // Total time of gameplay
    public float gameTime;

    // Counter for spawner
    public float spawntime;

    // Multiplier for count of units spawned each minute
    public int multiplier;

    // Counter to determine to load spawner counts
    public float loadSpawner;

};

