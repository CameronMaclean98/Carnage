using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;

[UpdateAfter(typeof(KeyboardInputSystem))]
public class CebilSpellCastSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }
    //Spell one Casts a banner which buffs attack of units near by
    struct CastAttackBuffBannerJob : IJobForEachWithEntity<CebilSpells>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayPosition;

        public void Execute(Entity entity, int index, ref CebilSpells spell)
        {
            var instance = CommandBuffer.Instantiate(spell.AttackBanner);
            CommandBuffer.SetComponent(instance, new Translation { Value = MouseRayPosition });
            CommandBuffer.AddComponent(instance, new TagAttackBuffBanner { });
            CommandBuffer.AddComponent(instance, new Banner { Location = MouseRayPosition, DurationTime = 10f });


        }
    }

    /*
    [RequireComponentTag(typeof(TagCebil))]
    struct CastDefenseBannerJob : IJobForEachWithEntity<CebilSpells>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayPosition;

        public void Execute(Entity entity, int index, ref CebilSpells spell)
        {
            var instance = CommandBuffer.Instantiate(spell.DefenseBanner);
            CommandBuffer.SetComponent(instance, new Translation { Value = MouseRayPosition });
            CommandBuffer.AddComponent(instance, new TagDefenseBuffBanner { });
            CommandBuffer.AddComponent(instance, new Banner { Location = MouseRayPosition, DurationTime = 10f });
        }
    }

    [RequireComponentTag(typeof(TagUnit))]
    [ExcludeComponent(typeof(CebilGlobalBuff))]
    struct GlobalBuffSpellJob : IJobForEachWithEntity<Translation>
    {
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, ref Translation location)
        {
            //Debug.Log("Adding Global buff to" + entity);
            CommandBuffer.AddComponent(entity, new CebilGlobalBuff { Duration = 10f, DefenseBuff = 25f, HealthBuff = 50f, Buffed = false });
        }
    }
    */
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();
        var keyboard = GetSingleton<SingletonKeyboardInput>();
        var timer = GetSingleton<SingletonSpellCoolDown>();
        timer.QSpellTimer += Time.deltaTime;
        SetSingleton<SingletonSpellCoolDown>(timer);

        if (keyboard.selectedSpell == KeyCode.Q && timer.QSpellTimer > timer.QSpellMaxTimer)
        {
            //Debug.Log("About to cast banner");
            
           if (mouse.RightClickDown == true)
            {
                keyboard.selectedSpell = KeyCode.None;
                timer.QSpellTimer = 0f;
                SetSingleton<SingletonSpellCoolDown>(timer);
                //Debug.Log("Creating Banner");
                //keyboard.Q_Key = false;
                SetSingleton<SingletonKeyboardInput>(keyboard);
                var defensebannerJob = new CastAttackBuffBannerJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    MouseRayPosition = mouse.CurrentMouseRaycastPosition
                }.ScheduleSingle(this, inputDeps);
                m_EntityCommandBufferSystem.AddJobHandleForProducer(defensebannerJob);
                 defensebannerJob.Complete();
            }
        }
        /*
        if (keyboard.selectedSpell == KeyCode.W && god.God == Type.Cebil)
        {
            //Debug.Log("current god =" + god.God);
            //Debug.Log("About to cast banner");
            keyboard.selectedSpell = KeyCode.None;
            if (mouse.RightClickDown == true)
            {

                //Debug.Log("Creating Banner");
                //keyboard.Q_Key = false;
                SetSingleton<SingletonKeyboardInput>(keyboard);
                var healbannerJob = new CastDefenseBannerJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    MouseRayPosition = mouse.CurrentMouseRaycastPosition
                }.ScheduleSingle(this, inputDeps);
                m_EntityCommandBufferSystem.AddJobHandleForProducer(healbannerJob);
                return healbannerJob;
            }
        }

        if(keyboard.selectedSpell == KeyCode.R && god.God == Type.Cebil)
        {
            keyboard.selectedSpell = KeyCode.None;
            SetSingleton<SingletonKeyboardInput>(keyboard);

            var cebilUltimate = new GlobalBuffSpellJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
            }.ScheduleSingle(this, inputDeps);
            m_EntityCommandBufferSystem.AddJobHandleForProducer(cebilUltimate);
            return cebilUltimate;
        }
        */
        return inputDeps;
    }
}
