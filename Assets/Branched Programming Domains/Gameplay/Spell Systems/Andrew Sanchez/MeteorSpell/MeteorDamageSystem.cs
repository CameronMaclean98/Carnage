//meteor does damage once it reaches the ground (y position is <= 0)

using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;


public class MeteorDamageSystem: JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    struct MeteorDamageJob : IJobForEachWithEntity<Translation, Health>
    {
        [ReadOnly] public NativeArray<Translation> meteorPos; //array that holds meteor position

        public void Execute(Entity entity, int index, ref Translation position, ref Health health)
        {
            foreach(Translation pos in meteorPos)
            {
                if (pos.Value.y <= 0)   //meteor does damage once it reaches the y <= 0 position
                {
                    if (math.distance(pos.Value, position.Value) < 100f)
                    {
                        health.Value -= 25;
                    }
                }
            }               
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        //reference to get meteor and their positions
        //make an array of meteor positions
        EntityQuery m_Pos = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<MeteorTag>());
        NativeArray<Translation> m_pos = m_Pos.ToComponentDataArray<Translation>(Allocator.Persistent);
        var handle = inputDeps;

        var meteorDamageJob = new MeteorDamageJob
        {
            meteorPos = m_pos            
        };

        handle = meteorDamageJob.Schedule(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(handle);

        handle.Complete();

        m_pos.Dispose();

        return handle;
    }

}
