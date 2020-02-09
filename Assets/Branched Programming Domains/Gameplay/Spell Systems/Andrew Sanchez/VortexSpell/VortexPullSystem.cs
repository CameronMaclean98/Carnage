
using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public class VortexPullSystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    //[BurstCompile]
    struct PullJob : IJobForEachWithEntity<MovementSpeed, Target, Translation, Rotation, Health>
    {
        [ReadOnly] public float deltaTime;
        [ReadOnly] public NativeArray<Translation> Pos; //array of the damaging Entities position

        public void Execute(Entity entity, int index, [ReadOnly] ref MovementSpeed movementSpeed, [ReadOnly] ref Target target, ref Translation position, ref Rotation rotation, ref Health health)
        {
            foreach (Translation pos in Pos)
            {
                var distance = math.distance(pos.Value, position.Value);
                var direction = math.normalize(pos.Value - position.Value);

                if (distance < 10f)
                {
                    target.Action = UnitAction.Defend;
                    health.Value -= .2f;
                    position.Value += direction * 4f * deltaTime;
                    rotation.Value = quaternion.LookRotation(target.Destination - position.Value, math.up());
                }
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        EntityQuery m_Pos = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<VortexSpellComponent>());
        NativeArray<Translation> m_pos = m_Pos.ToComponentDataArray<Translation>(Allocator.TempJob);

        if (m_pos.Length < 0)
            return inputDeps;

        var handle = inputDeps;

        var pullJob = new PullJob
        {
            Pos = m_pos,
            deltaTime = Time.deltaTime
        };

        handle = pullJob.Schedule(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(handle);

        handle.Complete();

        m_pos.Dispose();

        return pullJob.Schedule(this, inputDeps);
    }
}
