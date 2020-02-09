using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;

[UpdateAfter(typeof(KeyboardInputSystem))]
public class UnfreezeSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    struct RemoveFreezeJob : IJobForEachWithEntity<Translation, Health, MovementSpeed, FreezeComponent, ShaderComponent>
    {
        public EntityCommandBuffer CommandBuffer;
        [ReadOnly] public float dTime;

        public void Execute(Entity entity, int index, ref Translation pos, ref Health health, ref MovementSpeed move, ref FreezeComponent freeze, ref ShaderComponent shader)
        {
            freeze.freezeTimer -= dTime;

            if (freeze.freezeTimer <= 0)
            {
                shader.change = true;
                shader.Freeze = 0;
                CommandBuffer.SetComponent(entity, new MovementSpeed { Value = freeze.originalSpeed });
                CommandBuffer.RemoveComponent<FreezeComponent>(entity);
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var removeFreezeJob = new RemoveFreezeJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            dTime = Time.deltaTime
                
        }.ScheduleSingle(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(removeFreezeJob);

        return removeFreezeJob;
    }
}
