//destroy meteor once it reaches ground (y position is <= 0)

using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;


public class MeteorDestroySystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }


    [RequireComponentTag(typeof(MeteorTag))]
    struct MeteorDestroyJob : IJobForEachWithEntity<Translation>
    {
        //[ReadOnly] public float deltaTime;
        [ReadOnly] public EntityCommandBuffer CommandBuffer;

        public void Execute(Entity entity, int index, ref Translation position)
        {
            if (position.Value.y <= 0) //destroy meteor if meteor has reached the ground
                CommandBuffer.DestroyEntity(entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var meteorDestroyJob = new MeteorDestroyJob
        {
            //CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
        };

        var handle = meteorDestroyJob.Schedule(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(handle);

        return handle;
    }

}
