using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;


public class UnitDestroySystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    struct MeteorDamageJob : IJobForEachWithEntity<Translation, Health>
    {
        //[ReadOnly] public float deltaTime;
        [ReadOnly] public EntityCommandBuffer CommandBuffer;

        public void Execute(Entity entity, int index, ref Translation position, ref Health health)
        {
            if (health.Value <= 0)
                CommandBuffer.DestroyEntity(entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var meteorDamageJob = new MeteorDamageJob
        {
            //CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
        };

        var handle = meteorDamageJob.Schedule(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(handle);

        return handle;
    }

}
