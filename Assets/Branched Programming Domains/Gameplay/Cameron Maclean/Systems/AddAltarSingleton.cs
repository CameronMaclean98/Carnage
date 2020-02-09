using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[RequiresEntityConversion]
public class AddAltarSingleton : MonoBehaviour, IConvertGameObjectToEntity
{
    public int teamnum;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {



        var hp = new Health
        {
            Max = 2000,
            Value = 2000,
            Type = ArmorType.Medium
        };
        dstManager.AddComponentData(entity, hp);


        var tag = new TagBuilding
        {

        };
        dstManager.AddComponentData(entity, tag);

        var tag2 = new TagAltar
        {

        };
        dstManager.AddComponentData(entity, tag2);

        var team = new Team
        {
            Number = teamnum,
        };
        dstManager.AddComponentData(entity, team);


    }
}