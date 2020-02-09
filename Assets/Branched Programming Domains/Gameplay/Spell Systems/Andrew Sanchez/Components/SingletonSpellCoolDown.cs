using Unity.Entities;

public struct SingletonSpellCoolDown : IComponentData
{
    public float QSpellTimer;
    public float QSpellMaxTimer;

    public float WSpellTimer;
    public float WSpellMaxTimer;

    public float ESpellTimer;
    public float ESpellMaxTimer;

    public float RSpellTimer;
    public float RSpellMaxTimer;
}
