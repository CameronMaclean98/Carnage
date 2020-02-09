using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(KeyboardInputSystem))]
public class MovingDamageSpawnSystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagPlayer))]
    struct SpawnJob : IJobForEachWithEntity<MeteorSpawner>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayPosition;

        public void Execute(Entity entity, int index, ref MeteorSpawner spawner)
        {
            var instance = CommandBuffer.Instantiate(spawner.Prefab);

            CommandBuffer.SetComponent(instance, new Translation { Value = MouseRayPosition });
            CommandBuffer.AddComponent(instance, new MovementSpeed { Value = 5.0f });
            CommandBuffer.AddComponent(instance, new MovingDamageComponent { timer = 5.0f });
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();
        var keyboard = GetSingleton<SingletonKeyboardSpells>();
        var keyboardSpell = GetSingleton<SingletonKeyboardSpells>();
        var spellCoolDown = GetSingleton<SingletonSpellCoolDown>();

        var jobHandle = inputDeps;
        if (keyboardSpell.EKeyDown)
        {
            if (spellCoolDown.ESpellTimer <= 0)
            {
                var spawnJob = new SpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    MouseRayPosition = mouse.CurrentMouseRaycastPosition
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(spawnJob);

                spellCoolDown.ESpellTimer = spellCoolDown.ESpellMaxTimer;

                SetSingleton<SingletonSpellCoolDown>(spellCoolDown);

                return spawnJob;
            }
        }

        return jobHandle;
    }
}