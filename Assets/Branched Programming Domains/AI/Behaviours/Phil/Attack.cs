using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

/*
 * This IComponentData will hold all next states for player unit if in 'Attack' state.
 */
public struct Attack : IComponentData
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
