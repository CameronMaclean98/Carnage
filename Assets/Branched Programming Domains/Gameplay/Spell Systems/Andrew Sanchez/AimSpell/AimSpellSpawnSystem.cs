using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

[UpdateAfter(typeof(MouseInputSystem))]
public class AimSpellSpawnSystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagPlayer))]
    struct SpawnJob : IJobForEachWithEntity<Translation, AimSpellActivatePlayerComponent, MeteorSpawner>
    {
        [ReadOnly] public float3 endPoint;
        [ReadOnly] public EntityCommandBuffer entityCommandBuffer;

        public void Execute(Entity entity, int i, [ReadOnly] ref Translation position, [ReadOnly] ref AimSpellActivatePlayerComponent point, ref MeteorSpawner spawn)
        {
            var instance = entityCommandBuffer.Instantiate(spawn.Prefab);
            entityCommandBuffer.SetComponent(instance, new Translation { Value = point.initialPoint });
            entityCommandBuffer.AddComponent(instance, new MovementSpeed { Value = 10f });
            entityCommandBuffer.AddComponent(instance, new AimSpellComponent { timer = 2.5f, destination = (math.normalize(endPoint - point.initialPoint))});
            entityCommandBuffer.RemoveComponent<AimSpellActivatePlayerComponent>(entity);
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
                endPoint = mouse.CurrentMouseRaycastPosition,
                entityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
            };
            jobHandle = spawnJob.ScheduleSingle(this, inputDeps);
        }        

        m_EntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

        return jobHandle;
    }
}