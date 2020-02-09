﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct Spawn : IComponentData
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