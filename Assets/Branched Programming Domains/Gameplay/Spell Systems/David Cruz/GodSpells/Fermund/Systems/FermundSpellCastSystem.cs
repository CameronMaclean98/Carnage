using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;

public class FermundSpellCastSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    struct CastHealingWard : IJobForEachWithEntity<FermundSpells>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayPosition;

        public void Execute(Entity entity, int index, ref FermundSpells spell)
        {
            var instance = CommandBuffer.Instantiate(spell.HealWard);
            CommandBuffer.SetComponent(instance, new Translation { Value = MouseRayPosition });
            CommandBuffer.AddComponent(instance, new TagHealingWard { });
            CommandBuffer.AddComponent(instance, new Ward { Location = MouseRayPosition, Duration = 10f });
        }
    }


    [RequireComponentTag(typeof(TagSelected))]
    [ExcludeComponent(typeof(TagEnemyUnit))]
    struct CastFermundsLight : IJobForEachWithEntity<Translation>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayPosition;

        public void Execute(Entity entity, int index, ref Translation location)
        {
            CommandBuffer.AddComponent(entity, new FermundsLight { });
        }
    }

    struct CastFermundsDegenAura: IJobForEachWithEntity<FermundSpells>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayCastPosition;

        public void Execute(Entity entity, int index, ref FermundSpells spells)
        {
            Debug.Log("Spawning Aura");
            var instance = CommandBuffer.Instantiate(spells.Aura);
            CommandBuffer.SetComponent(instance, new Translation { Value = MouseRayCastPosition });
            CommandBuffer.AddComponent(instance, new TagAura { });
            CommandBuffer.AddComponent(instance, new FermundsAura { Duration = 10f });
        }
    }


    [RequireComponentTag(typeof(TagUnit))]
    [ExcludeComponent(typeof(FermundGlobalHeal))]
    struct CastFermundGlobalHeal : IJobForEachWithEntity<Translation>
    {
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, ref Translation c0)
        {
            CommandBuffer.AddComponent(entity, new FermundGlobalHeal { Duration = 5f, Value = 2f });
        }
    }





    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();
        var keyboard = GetSingleton<SingletonKeyboardInput>();
        var god = GetSingleton<SingletonGod>();

        if (keyboard.selectedSpell == KeyCode.Q && god.God == Type.Fermund)
        {
            //Debug.Log("About to cast banner");
            keyboard.selectedSpell = KeyCode.None;
            if (mouse.RightClickDown == true)
            {

                Debug.Log("Creating Ward");
                //keyboard.Q_Key = false;
                SetSingleton<SingletonKeyboardInput>(keyboard);
                var healingJob = new CastHealingWard
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    MouseRayPosition = mouse.CurrentMouseRaycastPosition
                }.ScheduleSingle(this, inputDeps);
                m_EntityCommandBufferSystem.AddJobHandleForProducer(healingJob);
                return healingJob;
            }
        }

        if (keyboard.selectedSpell == KeyCode.W && god.God == Type.Fermund)
        {
            keyboard.selectedSpell = KeyCode.None;

            if (mouse.LeftClickDown == true)
            {
                SetSingleton<SingletonKeyboardInput>(keyboard);

                var fermundsLight = new CastFermundsLight
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()

                }.ScheduleSingle(this, inputDeps);
                m_EntityCommandBufferSystem.AddJobHandleForProducer(fermundsLight);
                return fermundsLight;
            }
        }

        if (keyboard.selectedSpell == KeyCode.E && god.God == Type.Fermund)
        {
            keyboard.selectedSpell = KeyCode.None;
            if (mouse.RightClickDown == true)
            {
                Debug.Log("Creating Fermunds Aura");
                SetSingleton<SingletonKeyboardInput>(keyboard);

                var auraJob = new CastFermundsDegenAura
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    MouseRayCastPosition = mouse.CurrentMouseRaycastPosition
                }.ScheduleSingle(this, inputDeps);
                m_EntityCommandBufferSystem.AddJobHandleForProducer(auraJob);
                return auraJob;
            }
        }

        if(keyboard.selectedSpell == KeyCode.R && god.God == Type.Fermund)
        {
            keyboard.selectedSpell = KeyCode.None;
            SetSingleton<SingletonKeyboardInput>(keyboard);

            var fermundsUltimate = new CastFermundGlobalHeal
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
            }.ScheduleSingle(this, inputDeps);
            m_EntityCommandBufferSystem.AddJobHandleForProducer(fermundsUltimate);

            return fermundsUltimate;
        }

        return inputDeps;
    }
}
