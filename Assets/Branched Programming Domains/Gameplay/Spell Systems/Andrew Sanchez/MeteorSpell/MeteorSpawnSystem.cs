using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(KeyboardInputSystem))]
public class MeteorSpawnSystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    struct MeteorSpawnJob : IJobForEachWithEntity<MeteorSpawner>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayPosition;

        public void Execute(Entity entity, int index, ref MeteorSpawner spawner)
        {
            var instance = CommandBuffer.Instantiate(spawner.Prefab);

            CommandBuffer.SetComponent(instance, new Translation { Value = new float3(MouseRayPosition.x, MouseRayPosition.y + 500, MouseRayPosition.z) });
            CommandBuffer.AddComponent(instance, new MovementSpeed { Value = 25.0f });
            CommandBuffer.AddComponent(instance, new MeteorTag { });
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();
        var keyboard = GetSingleton<SingletonKeyboardSpells>();
        var spellCoolDown = GetSingleton<SingletonSpellCoolDown>();

        var jobHandle = inputDeps;
        if (keyboard.QKeyDown)
        {
            if (spellCoolDown.QSpellTimer <= 0)
            {
                var meteorSpawnJob = new MeteorSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    MouseRayPosition = mouse.CurrentMouseRaycastPosition
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(meteorSpawnJob);

                spellCoolDown.QSpellTimer = spellCoolDown.QSpellMaxTimer;

                SetSingleton<SingletonSpellCoolDown>(spellCoolDown);

                return meteorSpawnJob;
            }
        }

        return jobHandle;
    }
}