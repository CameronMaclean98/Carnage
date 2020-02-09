using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


//[UpdateAfter(typeof(CebilSpellCastSystem))]
public class CebilDefenseBannerSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagDefenseBuffBanner))]
    struct BannerLocationJob : IJobForEachWithEntity<Translation, Banner>
    {
        public NativeArray<float3> bannerLocation;
        public EntityCommandBuffer CommandBuffer;
        public float DeltaTime;
        public void Execute(Entity entity, int index, [ReadOnly] ref Translation location, [ReadOnly] ref Banner banner)
        {
            bannerLocation[0] = location.Value;
            if (banner.DurationTime > 0f)
            {
                banner.DurationTime -= 1 * DeltaTime;
            }
            if (banner.DurationTime <= 0f)
            {
                CommandBuffer.RemoveComponent<TagDefenseBuffBanner>(entity);
                CommandBuffer.RemoveComponent<Banner>(entity);
                CommandBuffer.DestroyEntity(entity);
                //CommandBuffer.AddComponent(entity, new TagDestroy { });
            }
        }
    }

    [RequireComponentTag(typeof(TagLegionnaire))]
    struct LegionnaireLocationsJob : IJobForEachWithEntity<Translation, Health>
    {
        public NativeArray<float3> bannerLocation;
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {
            //Debug.Log("Checking if legionnaire is near Legionnaire");
            var defenseDistance = math.distance(location.Value, bannerLocation[0]);

            if (defenseDistance <= 50f)
            {
                health.Type = ArmorType.Medium;
            }
            else
            {
                health.Type = ArmorType.Light;
            }



        }
    }

    [RequireComponentTag(typeof(TagMarksman))]
    struct MarksmanLocationsJob : IJobForEachWithEntity<Translation, Health>
    {
        public NativeArray<float3> bannerLocation;
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {
            //Debug.Log("Checking if marksman is near banner");
            var defenseDistance = math.distance(location.Value, bannerLocation[0]);

            if (defenseDistance <= 50f)
            {
                health.Type = ArmorType.Light;
            }
            else
            {
                health.Type = ArmorType.None;
            }



        }
    }
    [RequireComponentTag(typeof(TagPaladin))]
    struct PaladinLocationsJob : IJobForEachWithEntity<Translation, Health>
    {
        public NativeArray<float3> bannerLocation;
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {
            //Debug.Log("Checking if paladin is near banner");
            var defenseDistance = math.distance(location.Value, bannerLocation[0]);

            if (defenseDistance <= 50f)
            {
                health.Type = ArmorType.Fortified;
            }
            else
            {
                health.Type = ArmorType.Heavy;
            }
        }
    }

    [RequireComponentTag(typeof(TagKnight))]
    struct KnightsLocationJob : IJobForEachWithEntity<Translation, Health>
    {
        public NativeArray<float3> bannerLocation;
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {
            //Debug.Log("Checking if knight is near banner");
            var defenseDistance = math.distance(location.Value, bannerLocation[0]);

            if (defenseDistance > 50f)
            {
                health.Type = ArmorType.Heavy;
            }
            else
            {
                health.Type = ArmorType.Medium;
            }
            CommandBuffer.RemoveComponent<Buffing>(entity);
        }
    }

    [RequireComponentTag(typeof(TagSpellslinger))]
    struct SpellslingerLocationJob : IJobForEachWithEntity<Translation, Health>
    {
        public NativeArray<float3> bannerLocation;
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {
            //Debug.Log("Checking if spellslinger is near banner");
            var defenseDistance = math.distance(location.Value, bannerLocation[0]);

            if (defenseDistance > 50f)
            {
                health.Type = ArmorType.Light;
            }
            else
            {
                health.Type = ArmorType.None;
            }
            CommandBuffer.RemoveComponent<Buffing>(entity);
        }
    }

    [RequireComponentTag(typeof(TagGryphon))]
    struct GryphonLocationJob : IJobForEachWithEntity<Translation, Health>
    {
        public NativeArray<float3> bannerLocation;
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {
            //Debug.Log("Checking if gryphon is near banner");            //Debug.Log("Checking if gryphon is near banner");
            var defenseDistance = math.distance(location.Value, bannerLocation[0]);

            if (defenseDistance > 50f)
            {
                health.Type = ArmorType.Medium;
            }
            else
            {
                health.Type = ArmorType.Light;
            }
            CommandBuffer.RemoveComponent<Buffing>(entity);
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        NativeArray<float3> BannerLocation = new NativeArray<float3>(1, Allocator.TempJob);

        var bannerJob = new BannerLocationJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            bannerLocation = BannerLocation,
            DeltaTime = Time.deltaTime
        }.ScheduleSingle(this, inputDeps);
        bannerJob.Complete();

        if (BannerLocation[0].x != 0 && BannerLocation[0].y != 0 && BannerLocation[0].z != 0)
        {
            var legionnaireToDefend = new LegionnaireLocationsJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                bannerLocation = BannerLocation
            }.ScheduleSingle(this, inputDeps);
            legionnaireToDefend.Complete();

            var marksmanToDefend = new MarksmanLocationsJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                bannerLocation = BannerLocation
            }.ScheduleSingle(this, inputDeps);
            marksmanToDefend.Complete();

            var paladinToDefend = new PaladinLocationsJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                bannerLocation = BannerLocation
            }.ScheduleSingle(this, inputDeps);
            paladinToDefend.Complete();

            var knightToDefend = new KnightsLocationJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                bannerLocation = BannerLocation
            }.ScheduleSingle(this, inputDeps);
            knightToDefend.Complete();

            var spellslingerToDefend = new SpellslingerLocationJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                bannerLocation = BannerLocation
            }.ScheduleSingle(this, inputDeps);
            spellslingerToDefend.Complete();

            var gryphonToDefend = new GryphonLocationJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                bannerLocation = BannerLocation
            }.ScheduleSingle(this, inputDeps);
            gryphonToDefend.Complete();
        }
        BannerLocation.Dispose();

        return inputDeps;
    }
}
