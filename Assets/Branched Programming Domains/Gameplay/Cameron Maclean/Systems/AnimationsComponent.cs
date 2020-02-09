using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public enum Animations
{
    idle,
    attack01,
    attack02,
    walk,
    run,
    victory,
    gethit,
    death,
    defend,
    shieldbash,
    channeling
}

public enum UnitAudioSourceSpawnGameplayState
{
    NONE,
    IDLE,
    ATTACK_01,
    ATTACK_02,
    WALK,
    RUN,
    VICTORY,
    GETHIT,
    DEATH,
    DEFEND,
    SHIELDBASH,
    CHANNELING
}

public enum UnitAudioSourceSpawnVoiceActingState
{
    NONE,
    LINE_01,
    LINE_02,
    LINE_03,
    LINE_04,
    LINE_05,
    LINE_06
}
//public struct AnimationsComponent : IComponentData
public struct AnimationsComponent : IComponentData
{
    public Animations Current;
    public Animations Compare;
    public bool changeAnim;


    // For Gameplay
    public UnitAudioSourceSpawnGameplayState unitAudioSourceSpawnGameplayState;

    // For VoiceActing
    public UnitAudioSourceSpawnVoiceActingState unitAudioSourceSpawnVoiceActingState;

    // Position
    public float3 spawnAudioSourcePrefabPosition;
}
