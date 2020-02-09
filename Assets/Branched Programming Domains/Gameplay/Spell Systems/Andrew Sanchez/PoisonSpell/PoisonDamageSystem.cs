//system that deals poison damage, removes poison component when timer finishes

using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;

[UpdateAfter(typeof(KeyboardInputSystem))]
public class PoisonDamageSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    struct PoisonDamageJob : IJobForEachWithEntity<Translation, Health, MovementSpeed, PoisonComponent>
    {
        public EntityCommandBuffer CommandBuffer;
        [ReadOnly] public float dTime;

        public void Execute(Entity entity, int index, ref Translation pos, ref Health health, ref MovementSpeed move, ref PoisonComponent poison)
        {
            poison.poisonTimer -= dTime;    
            health.Value -= poison.poisonDamage;

            if (poison.poisonTimer <= 0)
            {                
                CommandBuffer.RemoveComponent<PoisonComponent>(entity);
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var poisonDamageJob = new PoisonDamageJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            dTime = Time.deltaTime

        }.ScheduleSingle(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(poisonDamageJob);

        return poisonDamageJob;
    }
}
