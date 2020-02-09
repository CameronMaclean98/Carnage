using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct FermundGlobalHeal : IComponentData
{
    public float Duration;
    public float Value;
}
