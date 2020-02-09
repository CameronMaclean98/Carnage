using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;

public class FermundsLightSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }


    [RequireComponentTag(typeof(FermundsLight))]
    struct FermundsLightLocation : IJobForEachWithEntity<Translation, Health>
    {
        public NativeArray<float3> lightsLocation;
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {
            health.Value = 110f;
            lightsLocation[0] = location.Value;
            CommandBuffer.RemoveComponent<FermundsLight>(entity);

        }
    }

    [RequireComponentTag(typeof(TagUnit))]
    [ExcludeComponent(typeof(FermundsLight))]
    struct FermundsLightHeal : IJobForEachWithEntity<Translation, Health>
    {
        public NativeArray<float3> lightsLocation;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {
            var lightDistance = math.distance(location.Value, lightsLocation[0]);
            var maxHealth = health.Max;
            var currentHealth = health.Value;
            var healValue = maxHealth * 0.25f;
            var lifeAfterHeal = currentHealth + healValue;

            if(lightDistance <= 10f)
            {
                if(currentHealth == maxHealth)
                {
                    return;
                }
                else
                {
                    if(lifeAfterHeal >= maxHealth)
                    {
                        health.Value = maxHealth;
                    }
                    else
                    {
                        health.Value = lifeAfterHeal;
                    }
                }
            }
        }
    }

    [RequireComponentTag(typeof(TagEnemyUnit))]
    struct FermundsLightDamage : IJobForEachWithEntity<Translation, Health>
    {
        public NativeArray<float3> lightsLocation;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {
            var lightDistance = math.distance(location.Value, lightsLocation[0]);
            if(lightDistance <= 20f)
            {
                health.Value -= 50f;
            }
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        NativeArray<float3> FermundsLightLocation = new NativeArray<float3>(1, Allocator.TempJob);
        var lightLocationJob = new FermundsLightLocation
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            lightsLocation = FermundsLightLocation,
          
        }.ScheduleSingle(this, inputDeps);
        lightLocationJob.Complete();

        if(FermundsLightLocation[0].x != 0 && FermundsLightLocation[0].y != 0 && FermundsLightLocation[0].z != 0)
        {
            var unitsToHeal = new FermundsLightHeal
            {
                lightsLocation = FermundsLightLocation
            }.ScheduleSingle(this, inputDeps);
            unitsToHeal.Complete();

            var unitsToDamage = new FermundsLightDamage
            {
                lightsLocation = FermundsLightLocation
            }.ScheduleSingle(this, inputDeps);
            unitsToDamage.Complete();
        }


        return inputDeps;
    }
}
