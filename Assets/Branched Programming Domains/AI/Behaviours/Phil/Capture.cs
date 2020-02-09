using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct Capture : IComponentData
{
    public enum enemyState
    {
        Patrol,
        Move,
        Attack,
        HoldPosition,
        Gather,
        Defend,
        Capture,
        Build,
        Channel,
        Spawn,
        Alert
    };
}
