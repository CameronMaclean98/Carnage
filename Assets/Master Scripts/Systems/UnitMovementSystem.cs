using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public class UnitMovementSystem : JobComponentSystem
{
    [BurstCompile]
    struct MoveJob : IJobForEach<MovementSpeed, Target, Translation, Rotation, AnimationsComponent>
    {
        [ReadOnly] public float deltaTime;

        public void Execute([ReadOnly] ref MovementSpeed movementSpeed, [ReadOnly] ref Target target, ref Translation position, ref Rotation rotation, ref AnimationsComponent animations)
        {
            var distance = math.distance(target.Destination, position.Value);
            var direction = math.normalize(target.Destination - position.Value);

            if (!(distance < 1) && target.Action == UnitAction.Move)
            {
                animations.Current = Animations.run;
                animations.unitAudioSourceSpawnGameplayState = UnitAudioSourceSpawnGameplayState.RUN;
                position.Value += direction * movementSpeed.Value * deltaTime;
                rotation.Value = quaternion.LookRotation(target.Destination - position.Value, math.up());
            }
            else
            {
                animations.Current = Animations.idle;
                animations.unitAudioSourceSpawnGameplayState = UnitAudioSourceSpawnGameplayState.IDLE;
            }
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
