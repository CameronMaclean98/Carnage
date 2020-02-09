using Unity.Entities;

public struct UnitSpawnerSingleton : IComponentData
{
    // The 7 types of Humans
    public int Knights;
    public int Paladins;
    public int SpellSlingers;
    public int Legionnaires;
    public int Marksmen;
    public int SiegeEngines;
    public int Griffins;

    // The 7 types of Orcs
    public int Brutes;
    public int Sluggers;
    public int Warlords;
    public int Raiders;
    public int Warlocks;
    public int Demolishers;
    public int Wyverns;

    public float spawntime;
}