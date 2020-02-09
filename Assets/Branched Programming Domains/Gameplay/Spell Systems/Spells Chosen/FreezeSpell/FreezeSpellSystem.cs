 //system that adds freeze component to units, does initial slight damage

using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;

[UpdateAfter(typeof(KeyboardInputSystem))]
public class FreezeSpellSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    struct AddFreezeComponentJob : IJobForEachWithEntity<Translation, Health, MovementSpeed, ShaderComponent>
    {
        public EntityCommandBuffer CommandBuffer;
        public float3 MouseRayPosition; //current mouse raycast position

        public void Execute(Entity entity, int index, ref Translation pos, ref Health health, ref MovementSpeed move, ref ShaderComponent shader)
        {

            if (math.distance(MouseRayPosition, pos.Value) < 30f)   //add freeze component to entity if its within range
            {
                shader.change = true;
                shader.Freeze = 1;
                //freeze component has timer for duration of freeze, also stores entities original movespeed so it can reassign it when unfrozen
                CommandBuffer.AddComponent(entity, new FreezeComponent { freezeTimer = 10f, originalSpeed = move.Value }); 
                CommandBuffer.SetComponent(entity, new MovementSpeed { Value = 0f });   //set entity speed to 0
                health.Value -= 15f;    //slight damage to entity
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();
        var keyboard = GetSingleton<SingletonKeyboardInput>();
        var timer = GetSingleton<SingletonSpellCoolDown>();
        timer.RSpellTimer += Time.deltaTime;
        SetSingleton<SingletonSpellCoolDown>(timer);
        if (keyboard.selectedSpell == KeyCode.R && timer.RSpellTimer > timer.RSpellMaxTimer)
        {


            if (mouse.RightClickDown == true)
            {
                keyboard.selectedSpell = KeyCode.None;
                timer.RSpellTimer = 0f;
                SetSingleton<SingletonSpellCoolDown>(timer);
                SetSingleton<SingletonKeyboardInput>(keyboard);



                var addFreezeJob = new AddFreezeComponentJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    MouseRayPosition = mouse.CurrentMouseRaycastPosition
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(addFreezeJob);

                addFreezeJob.Complete();
            }
        }
        
        return inputDeps;
    }
}