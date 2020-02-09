//System that deals damage gradually over time withing range of damage entity

using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public class MovingDamageSystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    
    struct MovingDamageJob : IJobForEachWithEntity<Translation, Health>
    {
        [ReadOnly] public NativeArray<Translation> Pos; //array of the damaging Entities position

        public void Execute(Entity entity, int index, ref Translation position, ref Health health)
        {
            foreach (Translation pos in Pos)    //deal damage if entity is close to the damaging entity
            {                
                if (math.distance(pos.Value, position.Value) < 100f)
                {
                    health.Value -= 0.2f;
                }                
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //reference for moving damage entity and its position
        //makes an array of moving damage entities position
        EntityQuery m_Pos = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<MovingDamageComponent>());
        NativeArray<Translation> m_pos = m_Pos.ToComponentDataArray<Translation>(Allocator.Persistent);

        var handle = inputDeps;

        var damageJob = new MovingDamageJob
        {
            Pos = m_pos
        };

        handle = damageJob.Schedule(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(handle);

        handle.Complete();

        m_pos.Dispose();

        return handle;
    }

}
