using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public class AimSpellMovementSystem : JobComponentSystem
{
    [BurstCompile]
    struct MoveJob : IJobForEach<MovementSpeed, Translation, AimSpellComponent>
    {
        [ReadOnly] public float deltaTime;

        public void Execute([ReadOnly] ref MovementSpeed movementSpeed, ref Translation position, ref AimSpellComponent aimSpell)
        {                     
            position.Value += aimSpell.destination * movementSpeed.Value * deltaTime;               
            
            aimSpell.timer -= deltaTime;
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
