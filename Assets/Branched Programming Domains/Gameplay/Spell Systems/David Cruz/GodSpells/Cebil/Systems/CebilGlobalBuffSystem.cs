using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Transforms;

public class CebilGlobalBuffSystem : JobComponentSystem
{

    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagUnit))]
    struct GlobalBuffJob : IJobForEachWithEntity<Translation, CebilGlobalBuff, Range>
    {
        public float DeltaTime;
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, ref Translation c0, ref CebilGlobalBuff globalBuff, ref Range range)
        {
            var currentRange = range.Value;
            if (globalBuff.Duration > 0)
            {
                if (globalBuff.Buffed == false)
                {
                    Debug.Log("Buffing Rate");
                    range.Value = currentRange*2f;
                    globalBuff.Buffed = true;
                }
                else {
                    Debug.Log("Already Buffed");
                }
                globalBuff.Duration -= 1f * DeltaTime;
            }

            if (globalBuff.Duration <= 0)
            {
                range.Value = currentRange / 2f;
                CommandBuffer.RemoveComponent<CebilGlobalBuff>(entity);
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var buffJob = new GlobalBuffJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            DeltaTime = Time.deltaTime
        }.ScheduleSingle(this, inputDeps);
        m_EntityCommandBufferSystem.AddJobHandleForProducer(buffJob);

        return buffJob;
    }
}