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

    struct SpawnJob : IJobForEachWithEntity<LightningSpawner>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayPosition;

        public void Execute(Entity entity, int index, ref LightningSpawner spawner)
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
        var keyboard = GetSingleton<SingletonKeyboardInput>();
        var timer = GetSingleton<SingletonSpellCoolDown>();
        timer.ESpellTimer += Time.deltaTime;
        SetSingleton<SingletonSpellCoolDown>(timer);
        var jobHandle = inputDeps;


        if (keyboard.selectedSpell == KeyCode.E && timer.ESpellTimer > timer.ESpellMaxTimer)
        {
            
            if (mouse.RightClickDown == true)
            {
                keyboard.selectedSpell = KeyCode.None;
                timer.ESpellTimer = 0f;
                SetSingleton<SingletonSpellCoolDown>(timer);
                var spawnJob = new SpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    MouseRayPosition = mouse.CurrentMouseRaycastPosition
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(spawnJob);
                return spawnJob;
            }
        }

        return jobHandle;
    }
}