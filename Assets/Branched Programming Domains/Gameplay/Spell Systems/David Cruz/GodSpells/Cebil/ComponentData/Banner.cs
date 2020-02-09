using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct Banner : IComponentData
{
    public float DurationTime;
    public float3 Location;
}
