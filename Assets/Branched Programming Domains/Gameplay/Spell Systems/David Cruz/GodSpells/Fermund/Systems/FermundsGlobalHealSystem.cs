using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Transforms;

public class FermundsGlobalHealSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagUnit))]
    struct FermundGlobalHealJob : IJobForEachWithEntity<Health, FermundGlobalHeal>
    {
        public EntityCommandBuffer CommandBuffer;
        public float DeltaTime;
        public void Execute(Entity entity, int index, ref Health health, ref FermundGlobalHeal globalHeal)
        {
            var currentHealth = health.Value;
            var maxHealth = health.Max;
            if (globalHeal.Duration > 0)
            {
              if(currentHealth < maxHealth)
                {
                    health.Value += globalHeal.Value * DeltaTime;
                }
              else if (currentHealth >= maxHealth)
                {
                    health.Value = maxHealth;
                }
                globalHeal.Duration -= 1f * DeltaTime;
            }

            if (globalHeal.Duration <= 0)
            {
                CommandBuffer.RemoveComponent<FermundGlobalHeal>(entity);
            }
        }
    }



    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var healJob = new FermundGlobalHealJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            DeltaTime = Time.deltaTime
        }.ScheduleSingle(this, inputDeps);
        m_EntityCommandBufferSystem.AddJobHandleForProducer(healJob);

        return healJob;
    }
}
