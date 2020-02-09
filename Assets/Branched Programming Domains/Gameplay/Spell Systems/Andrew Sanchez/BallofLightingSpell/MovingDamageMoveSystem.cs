//system to move the damage entity, entity follows current mouse raycast position

using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public class MovingDamageMoveSystem : JobComponentSystem
{
    [BurstCompile]
    struct MoveJob : IJobForEach<MovementSpeed,Translation, MovingDamageComponent>
    {
        [ReadOnly] public float deltaTime;
        public float3 MouseRayPosition; //current mouse raycast position

        public void Execute([ReadOnly] ref MovementSpeed movementSpeed, ref Translation position, ref MovingDamageComponent moveDamage)
        {
            var distance = math.distance(MouseRayPosition, position.Value); //distance between the entity and the current mouse raycast position
            var direction = math.normalize(MouseRayPosition - position.Value);

            moveDamage.timer -= deltaTime;  //timer count for duration of entity (destroys entity at timer <= 0 in another system)

            if (!(distance < 1))    
            {
                position.Value += direction * movementSpeed.Value * deltaTime;  //move entity towards mouse raycast position
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouseInput = GetSingleton<SingletonMouseInput>();

        var moveJob = new MoveJob
        {
            MouseRayPosition = mouseInput.CurrentMouseRaycastPosition,
            deltaTime = Time.deltaTime
        };

        return moveJob.Schedule(this, inputDeps);
    }
}
