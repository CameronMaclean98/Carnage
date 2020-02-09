using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

[UpdateAfter(typeof(MouseInputSystem))]
public class VortexSpellActivateSystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagPlayer))]
    struct AddSpellActivateJob : IJobForEachWithEntity<Translation>
    {
        [ReadOnly] public EntityCommandBuffer entityCommandBuffer;

        public void Execute(Entity entity, int index, ref Translation position)
        {
            entityCommandBuffer.AddComponent(entity, new VortexSpellActiveTag {  });
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();

        var jobHandle = inputDeps;

        if (Input.GetKeyDown(KeyCode.O))    //TODO-- Change input to a singleton spell key and add requirecomponent of corresponding God tag
        {
            var addSpellActivateJob = new AddSpellActivateJob
            {
                entityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
            };

            jobHandle = addSpellActivateJob.Schedule(this, inputDeps);
        }

        m_EntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

        return jobHandle;
    }
}