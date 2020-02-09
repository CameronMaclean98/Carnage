using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[RequiresEntityConversion]
public class AddBruteSingleton : MonoBehaviour, IConvertGameObjectToEntity
{
    public float unitspeed = 50.0f;
    public int teamnum;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        /*
 * This is commented because we only want it to be selected if it is spawned
        var select = new Selectable
        {
            SelectSize = 5.0f
        };
        dstManager.AddComponentData(entity, select);
*/
        var pool = new TagUnitPool
        {

        };

        dstManager.AddComponentData(entity, pool);

        var targetinput = new Target
        {
            Destination = float3.zero,


        };
        dstManager.AddComponentData(entity, targetinput);

        var ratenum = new Rate
        {
            Value = 0,
            Cooldown = 1,
            Max = 20
        };
        dstManager.AddComponentData(entity, ratenum);

        var damagenum = new Damage
        {
            Max = 75,
            Min = 45,
            Type = AttackType.Slashing
        };
        dstManager.AddComponentData(entity, damagenum);

        var rangenum = new Range
        {
            MaxEngagement = 20.0f,
            Max = 5.0f,
            Min = 1.0f,
            Value = 4f
        };
        dstManager.AddComponentData(entity, rangenum);

        var hp = new Health
        {
            Max = 200,
            Value = 200,
            Type = ArmorType.Light
        };
        dstManager.AddComponentData(entity, hp);


        var movementSpeed = new MovementSpeed
        {
            Value = unitspeed
        };
        dstManager.AddComponentData(entity, movementSpeed);

        var copyTransform = new CopyTransformToGameObject { };
        dstManager.AddComponentData(entity, copyTransform);

        var tag = new TagBrute
        {
        };
        dstManager.AddComponentData(entity, tag);

        var tag2 = new TagMeleeUnit
        {

        };
        dstManager.AddComponentData(entity, tag2);

        var tag3 = new TagUnit
        {

        };
        dstManager.AddComponentData(entity, tag3);

        var team = new Team
        {
            Number = teamnum,
        };
        dstManager.AddComponentData(entity, team);

        var animations = new AnimationsComponent
        {

        };
        dstManager.AddComponentData(entity, animations);
        var shader = new ShaderComponent
        {
            change = false,
            HealBuff = 0,
            SpeedBuff = 0,
            Freeze = 0
        };
        dstManager.AddComponentData(entity, shader);
    }
}
