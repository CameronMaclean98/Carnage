using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

[UpdateAfter(typeof(MouseInputSystem))]
public class VortexSpellSpawnSystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagPlayer), typeof(VortexSpellActiveTag))]
    struct SpawnJob : IJobForEachWithEntity<Translation, MeteorSpawner>
    {
        [ReadOnly] public EntityCommandBuffer entityCommandBuffer;
        [ReadOnly] public float3 spawnPoint;

        public void Execute(Entity entity, int i, [ReadOnly] ref Translation position, ref MeteorSpawner spawn)
        {
            var instance = entityCommandBuffer.Instantiate(spawn.Prefab);
            entityCommandBuffer.SetComponent(instance, new Translation { Value = new float3(spawnPoint.x, spawnPoint.y+3f, spawnPoint.z) });
            entityCommandBuffer.AddComponent(instance, new MovementSpeed { Value = 10f });
            entityCommandBuffer.AddComponent(instance, new VortexSpellComponent { timer = 8f });
            entityCommandBuffer.RemoveComponent<VortexSpellActiveTag>(entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();

        var jobHandle = inputDeps;

        if (mouse.LeftClickDown)
        {
            var spawnJob = new SpawnJob
            {
                spawnPoint = mouse.CurrentMouseRaycastPosition,
                entityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
            };
            jobHandle = spawnJob.ScheduleSingle(this, inputDeps);
        }

        m_EntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

        return jobHandle;
    }
}