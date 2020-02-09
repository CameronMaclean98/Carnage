using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public enum Type {
    None,
    Cebil, 
    Fermund,
    Branno,
    MalBain,
    Theros,
    Nima
}

public struct SingletonGod : IComponentData
{
    public Type God;
}
