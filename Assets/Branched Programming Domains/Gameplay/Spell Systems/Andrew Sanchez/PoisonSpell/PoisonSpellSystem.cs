//system that adds poison component to entity if within range

using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;

[UpdateAfter(typeof(KeyboardInputSystem))]
public class PoisonSpellSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    struct AddPoisonJob : IJobForEachWithEntity<Translation, Health, MovementSpeed>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayPosition; //current mouse raycast position

        public void Execute(Entity entity, int index, ref Translation pos, ref Health health, ref MovementSpeed move)
        {

            if (math.distance(MouseRayPosition, pos.Value) < 10f)
            {
                CommandBuffer.AddComponent(entity, new PoisonComponent { poisonTimer = 10f, poisonDamage = 0.1f });
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();
        var keyboard = GetSingleton<SingletonKeyboardSpells>();

        if (Input.GetKeyDown(KeyCode.T))
        {
            var addPoisonJob = new AddPoisonJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                MouseRayPosition = mouse.CurrentMouseRaycastPosition
            }.ScheduleSingle(this, inputDeps);

            m_EntityCommandBufferSystem.AddJobHandleForProducer(addPoisonJob);

            return addPoisonJob;
        }

        return inputDeps;
    }
}