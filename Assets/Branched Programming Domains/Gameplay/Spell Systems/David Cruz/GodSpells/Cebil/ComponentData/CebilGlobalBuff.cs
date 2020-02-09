using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct CebilGlobalBuff : IComponentData
{
    public float Duration;
    public float HealthBuff;
    public float DefenseBuff;
    public bool Buffed;
}
