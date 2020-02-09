using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public class TeleportUnitSystem : JobComponentSystem
{
    [BurstCompile]
    [RequireComponentTag(typeof(TagSelected))]
    struct TeleportJob : IJobForEachWithEntity<Target, Translation, Rotation>
    {    
        [ReadOnly] public float3 teleportDestination;

        public void Execute(Entity entity, int index, [ReadOnly] ref Target target, ref Translation position, ref Rotation rotation)
        {
            var distance = math.distance(teleportDestination, position.Value);
            
            if (!(distance < 1))// && target.Action != UnitAction.Move)
            {
                position.Value = teleportDestination;
                //position.Value += teleportDestination;
                rotation.Value = quaternion.LookRotation(target.Destination - position.Value, math.up());
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouseInput = GetSingleton<SingletonMouseInput>();
        
        var jobHandle = inputDeps;

        if (Input.GetKeyDown(KeyCode.U))
        {
            var teleportJob = new TeleportJob
            {
                teleportDestination = mouseInput.CurrentMouseRaycastPosition               
            };

            jobHandle = teleportJob.Schedule(this, inputDeps);

            jobHandle.Complete();

        }

        return jobHandle;
    }
}
