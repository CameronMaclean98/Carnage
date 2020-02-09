using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(CebilSpellCastSystem))]

public class CebilAttackBannerSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagAttackBuffBanner))]
    struct BannerLocationJob : IJobForEachWithEntity<Translation, Banner>
    {
        public NativeArray<float3> bannerLocation;
        public NativeArray<bool> bannerDestroyed;
        public EntityCommandBuffer CommandBuffer;
        public float DeltaTime;
        public void Execute(Entity entity, int index, [ReadOnly] ref Translation location, [ReadOnly] ref Banner banner)
        {
            bannerLocation[0] = location.Value;
            if (banner.DurationTime > 0f)
            {
                bannerDestroyed[0] = false;
                banner.DurationTime -= 1 * DeltaTime;
            }
            if (banner.DurationTime <= 0f)
            {
                bannerDestroyed[0] = true;
                CommandBuffer.RemoveComponent<TagAttackBuffBanner>(entity);
                CommandBuffer.RemoveComponent<Banner>(entity);
                CommandBuffer.DestroyEntity(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagLegionnaire))]
    [ExcludeComponent(typeof(CebilGlobalBuff))]
    struct LegionnaireLocationsJob : IJobForEachWithEntity<Translation, Range, Rate, ShaderComponent>
    {
        public NativeArray<float3> bannerLocation;
        public NativeArray<bool> bannerDestroyed;
        public void Execute(Entity entity, int index, ref Translation location, ref Range range, ref Rate rate, ref ShaderComponent shader)
        {

            var bannerBuffDistance = math.distance(location.Value, bannerLocation[0]);
            if (bannerDestroyed[0] == true)
            {
                shader.change = true;
                shader.SpeedBuff = 0;
                range.Value = 5f;
                //rate.Value = 1f;
            }
            else
            {

                if (bannerBuffDistance <= 30f)
                {
                    shader.change = true;
                    shader.SpeedBuff = 1;
                    //rate.Value = 2.0f;
                    range.Value = 10f;
                }
                else
                {
                    //rate.Value = 1.3f;
                    range.Value = 5f;
                }
            }

        }
    }

    [RequireComponentTag(typeof(TagMarksman))]
    [ExcludeComponent(typeof(CebilGlobalBuff))]
    struct MarksmanLocationsJob : IJobForEachWithEntity<Translation, Range, Rate, ShaderComponent>
    {
        public NativeArray<float3> bannerLocation;
        public NativeArray<bool> bannerDestroyed;
        public void Execute(Entity entity, int index, ref Translation location, ref Range range, ref Rate rate, ref ShaderComponent shader)
        {

            var bannerBuffDistance = math.distance(location.Value, bannerLocation[0]);
            if (bannerDestroyed[0] == true)
            {
                shader.change = true;
                shader.SpeedBuff = 0;
                range.Value = 35f;
                //rate.Value = 1f;
            }
            else
            {
                if (bannerBuffDistance <= 20f)
                {
                    shader.change = true;
                    shader.SpeedBuff = 1;
                    // rate.Value = 1.5f;
                    range.Value = 45f;
                }
                else
                {
                    range.Value = 35f;
                    //rate.Value = 1f;
                }
            }

        }
    }

    [RequireComponentTag(typeof(TagPaladin))]
    [ExcludeComponent(typeof(CebilGlobalBuff))]
    struct PaladinLocationsJob : IJobForEachWithEntity<Translation, Range, Rate, ShaderComponent>
    {
        public NativeArray<float3> bannerLocation;
        public NativeArray<bool> bannerDestroyed;
        public void Execute(Entity entity, int index, ref Translation location, ref Range range, ref Rate rate, ref ShaderComponent shader)
        {
            //Debug.Log("Buffing Paladin");
            var bannerBuffDistance = math.distance(location.Value, bannerLocation[0]);
            if (bannerDestroyed[0] == true)
            {

                shader.change = true;
                shader.SpeedBuff = 0;
                range.Value = 5f;
                //rate.Value = 1f;
            }
            else
            {
                if (bannerBuffDistance <= 20f)
                {

                    shader.change = true;
                    shader.SpeedBuff = 1;
                    range.Value = 10f;
                    //rate.Value = 2.2f;
                }
                else
                {
                    Debug.Log("Banner Paladin Distance");
                    range.Value = 5f;
                    //rate.Value = 1.2f;
                }
            }

        }
    }

    [RequireComponentTag(typeof(TagKnight))]
    [ExcludeComponent(typeof(CebilGlobalBuff))]
    struct KnightLocationsJob : IJobForEachWithEntity<Translation, Range, Rate, ShaderComponent>
    {
       
        public NativeArray<float3> bannerLocation;
        public NativeArray<bool> bannerDestroyed;
        public void Execute(Entity entity, int index, ref Translation location, ref Range range, ref Rate rate, ref ShaderComponent shader)
        {

            var bannerBuffDistance = math.distance(location.Value, bannerLocation[0]);
            if (bannerDestroyed[0] == true)
            {

                shader.change = true;
                shader.SpeedBuff = 0;
                range.Value = 5f;
                //rate.Value = 1f;
            }
            else
            {
                if (bannerBuffDistance <= 20f)
                {

                    shader.change = true;
                    shader.SpeedBuff = 1;
                    Debug.Log("knight near banner");
                    //rate.Value = 10f;
                    range.Value = 10f;
                }
                else
                {
                    range.Value = 5f;
                }
            }
           

        }
    }



    [RequireComponentTag(typeof(TagSpellslinger))]
    [ExcludeComponent(typeof(CebilGlobalBuff))]
    struct SpellslingerLocationsJob : IJobForEachWithEntity<Translation, Range, Rate, ShaderComponent>
    {
        public NativeArray<float3> bannerLocation;
        public NativeArray<bool> bannerDestroyed;
        public void Execute(Entity entity, int index, ref Translation location, ref Range range, ref Rate rate, ref ShaderComponent shader)
        {

            var bannerBuffDistance = math.distance(location.Value, bannerLocation[0]);
            if (bannerDestroyed[0] == true)
            {

                shader.change = true;
                shader.SpeedBuff = 0;
                range.Value = 25f;
                //rate.Value = 1f;
            }
            else
            {
                if (bannerBuffDistance <= 20f)
                {

                    shader.change = true;
                    shader.SpeedBuff = 1;
                    range.Value = 45f;
                    //rate.Value = 1.4f;
                }
                else
                {
                    range.Value = 25f;
                    //rate.Value = 0.7f;
                }
            }

        }
    }


    [RequireComponentTag(typeof(TagGryphon))]
    [ExcludeComponent(typeof(CebilGlobalBuff))]
    struct GryphonLocationsJob : IJobForEachWithEntity<Translation, Range, Rate, ShaderComponent>
    {
        public NativeArray<float3> bannerLocation;
        public NativeArray<bool> bannerDestroyed;
        public void Execute(Entity entity, int index, ref Translation location, ref Range range, ref Rate rate, ref ShaderComponent shader)
        {

            var bannerBuffDistance = math.distance(location.Value, bannerLocation[0]);
            if (bannerDestroyed[0] == true)
            {

                shader.change = true;
                shader.SpeedBuff = 0;
                range.Value = 5f;
               // rate.Value = 1.25f;
            }
            else
            {
                if (bannerBuffDistance <= 20f)
                {

                    shader.change = true;
                    shader.SpeedBuff = 1;
                    range.Value = 10f;
                    //rate.Value = 2.5f;
                }
                else
                {
                    range.Value = 5f;
                }
            }
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        NativeArray<float3> BannerLocation = new NativeArray<float3>(1, Allocator.TempJob);
        NativeArray<bool> BannerDestroyed = new NativeArray<bool>(1, Allocator.TempJob);
        BannerLocation[0] = float3.zero; 

        var bannerJob = new BannerLocationJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            bannerLocation = BannerLocation,
            bannerDestroyed = BannerDestroyed,
            DeltaTime = Time.deltaTime
        }.ScheduleSingle(this, inputDeps);
        bannerJob.Complete();

        if (BannerLocation[0].x != 0 && BannerLocation[0].y != 0 && BannerLocation[0].z != 0)
        {
            Debug.Log("Banner Located");
            var legionnaireToBuff = new LegionnaireLocationsJob
            {
                bannerLocation = BannerLocation,
                bannerDestroyed = BannerDestroyed
            }.ScheduleSingle(this, inputDeps);
            legionnaireToBuff.Complete();

            var marksmanToBuff = new MarksmanLocationsJob
            {
                bannerLocation = BannerLocation,
                bannerDestroyed = BannerDestroyed
            }.ScheduleSingle(this, inputDeps);
            marksmanToBuff.Complete();

            var paladinToBuff = new PaladinLocationsJob
            {
                bannerLocation = BannerLocation,
                bannerDestroyed = BannerDestroyed
            }.ScheduleSingle(this, inputDeps);
            paladinToBuff.Complete();

            var knightToBuff = new KnightLocationsJob
            {
                bannerLocation = BannerLocation,
                bannerDestroyed = BannerDestroyed
            }.ScheduleSingle(this, inputDeps);
            knightToBuff.Complete();

            var spellslingerToBuff = new SpellslingerLocationsJob
            {
                bannerLocation = BannerLocation,
                bannerDestroyed = BannerDestroyed
            }.ScheduleSingle(this, inputDeps);
            spellslingerToBuff.Complete();

            var gryphonToBuff = new GryphonLocationsJob
            {
                bannerLocation = BannerLocation,
                bannerDestroyed = BannerDestroyed
            }.ScheduleSingle(this, inputDeps);
            gryphonToBuff.Complete();
        }
        BannerLocation.Dispose();
        BannerDestroyed.Dispose();

        return inputDeps;
    }
}
