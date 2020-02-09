using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;


public class VortexSpellDestroySystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    struct DestroyJob : IJobForEachWithEntity<Translation, VortexSpellComponent>
    {
        [ReadOnly] public float deltaTime;
        [ReadOnly] public EntityCommandBuffer CommandBuffer;

        public void Execute(Entity entity, int index, ref Translation position, ref VortexSpellComponent vortex)
        {
            vortex.timer -= deltaTime;

            if (vortex.timer <= 0)
                CommandBuffer.DestroyEntity(entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var destroyJob = new DestroyJob
        {
            //CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
            deltaTime = Time.deltaTime,
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
        };

        var handle = destroyJob.Schedule(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(handle);

        return handle;
    }

}
