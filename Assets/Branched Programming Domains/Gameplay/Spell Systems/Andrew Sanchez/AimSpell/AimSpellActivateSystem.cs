using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

[UpdateAfter(typeof(MouseInputSystem))]
public class AimSpellActivateSystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagPlayer))]
    struct AddAimSpellActivateJob : IJobForEachWithEntity<Translation>
    {
        [ReadOnly] public EntityCommandBuffer entityCommandBuffer;
        [ReadOnly] public float3 SpawnPoint;

        public void Execute(Entity entity, int index, ref Translation position)
        {
            entityCommandBuffer.AddComponent(entity, new AimSpellActivatePlayerComponent { initialPoint = SpawnPoint });            
        }
    }    

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();

        var jobHandle = inputDeps;

        if (Input.GetKeyDown(KeyCode.I))    //TODO-- Change input to a singleton spell key and add requirecomponent of corresponding God tag
        {
            var aimSpellActivateJob = new AddAimSpellActivateJob
            {
                SpawnPoint = mouse.CurrentMouseRaycastPosition,
                entityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
            };

            jobHandle = aimSpellActivateJob.Schedule(this, inputDeps);
        }

        m_EntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

        return jobHandle;
    }
}