using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[RequiresEntityConversion]
public class AddBuildingSingletons : MonoBehaviour, IConvertGameObjectToEntity
{
    public int hpnum;
    public int teamnum;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {

       

        var hp = new Health
        {
            Max = hpnum,
            Value = hpnum,
            Type = ArmorType.Heavy
        };
        dstManager.AddComponentData(entity, hp);
        

        var tag = new TagBuilding
        {

        };
        dstManager.AddComponentData(entity, tag);


        var team = new Team
        {
            Number = teamnum,
        };
        dstManager.AddComponentData(entity, team);


    }
}
