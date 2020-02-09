using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public class MeteorMovementSystem : JobComponentSystem
{
    [BurstCompile]
    [RequireComponentTag(typeof(MeteorTag))]
    struct MoveJob : IJobForEach<MovementSpeed, Translation>
    {
        [ReadOnly] public float deltaTime;

        public void Execute([ReadOnly] ref MovementSpeed movementSpeed, ref Translation position)
        {
            //var distance = math.distance(target.Destination, position.Value);
            //var direction = math.normalize(target.Destination - position.Value);
            position.Value.y -= movementSpeed.Value * deltaTime * 25;
            //rotation.Value = quaternion.LookRotation(target.Destination - position.Value, math.up());            
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var moveJob = new MoveJob
        {
            deltaTime = Time.deltaTime
        };

        return moveJob.Schedule(this, inputDeps);
    }
}
