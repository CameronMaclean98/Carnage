using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;

public class FermundsAuraSystem : JobComponentSystem
{

    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagAura))]
    struct AuraLocation : IJobForEachWithEntity<Translation, FermundsAura>
    {
        public NativeArray<float3> auraLocation;
        public EntityCommandBuffer CommandBuffer;
        public float DeltaTime;

        public void Execute(Entity entity, int index, ref Translation location, ref FermundsAura aura)
        {
            auraLocation[0] = location.Value;
            if (aura.Duration > 0f)
            {
                aura.Duration -= 1 * DeltaTime;
            }
            if (aura.Duration <= 0f)
            {
                CommandBuffer.RemoveComponent<TagAura>(entity);
                CommandBuffer.RemoveComponent<FermundsAura>(entity);
                CommandBuffer.DestroyEntity(entity);
                //CommandBuffer.AddComponent(entity, new TagDestroy { });
            }
        }
    }

    [RequireComponentTag(typeof(TagEnemyUnit))]
    struct FermundsAuraDegenjob : IJobForEachWithEntity<Translation, MovementSpeed, Health>
    {
        public NativeArray<float3> auraLocation;
        public float DeltaTime;
        public void Execute(Entity entity, int index, ref Translation location, ref MovementSpeed speed, ref Health health)
        {
            var auraDistance = math.distance(auraLocation[0], location.Value);
            Debug.Log("Debuffing Enemy Unit");
            if(auraDistance <= 40f)
            {
                health.Value -= 1f*DeltaTime; 
                speed.Value -= 1f*DeltaTime;
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        NativeArray<float3> AurasLocation = new NativeArray<float3>(1, Allocator.TempJob);

        var auraLocation = new AuraLocation
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            auraLocation = AurasLocation,
            DeltaTime = Time.deltaTime
        }.ScheduleSingle(this, inputDeps);
        auraLocation.Complete();
        
        if ( AurasLocation[0].x != 0 && AurasLocation[0].y != 0 && AurasLocation[0].z != 0)
        {
            Debug.Log("About to debuff enemy unit");
            var auraDegenJob = new FermundsAuraDegenjob
            {
                auraLocation = AurasLocation,
                DeltaTime = Time.deltaTime
            }.ScheduleSingle(this, inputDeps);
            auraDegenJob.Complete();
            return auraDegenJob;
        }

        return inputDeps;
    }
}
