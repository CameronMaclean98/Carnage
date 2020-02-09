//system that heals units within range

using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public class HealSystem : JobComponentSystem
{
    [BurstCompile]
    struct HealJob : IJobForEach<Translation, Health, Selectable>
    {
        [ReadOnly] public float3 currentMouseRayPosition;

        public void Execute(ref Translation position, ref Health health, ref Selectable select)
        {
            //add health if within range and health is less than 100 (will need to be changed to if health is below max health)
            if (math.distance(currentMouseRayPosition, position.Value) <= 100 && health.Value < 100f )
            {
                if (health.Value > 75f) //will need to use max health 
                {
                    health.Value = 100f;
                }
                else
                    health.Value += 25f;
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouseInput = GetSingleton<SingletonMouseInput>();
        var keyboard = GetSingleton<SingletonKeyboardSpells>();
        var jobHandle = inputDeps;
        
        if (keyboard.WKeyDown)
        {
            var healJob = new HealJob
            {
                currentMouseRayPosition = mouseInput.CurrentMouseRaycastPosition
            };
            return healJob.Schedule(this, inputDeps);
        }
        return jobHandle;
    }
}
