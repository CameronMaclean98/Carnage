
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(KeyboardInputSystem))]

public class AISpawnBarrier : EntityCommandBufferSystem { }

// This entire code has been Edited by Cameron Maclean. It is a mess, but it works so it is fine for now woohoo
public class AISpawnSystem : JobComponentSystem
{
    // BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    AISpawnBarrier m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
        // m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        m_EntityCommandBufferSystem = World.GetExistingSystem<AISpawnBarrier>();

    }


    [RequireComponentTag(typeof(TagPaladin), typeof(TagLegionnaire))]
    struct PaladinSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 250;
                health.Value = 250;
                health.Type = ArmorType.Heavy;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagSlugger), typeof(TagLegionnaire))]
    struct SluggerSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 75;
                health.Value = 75;
                health.Type = ArmorType.Light;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }
    [RequireComponentTag(typeof(TagUnitPool), typeof(TagLegionnaire))]
    struct LegionnaireSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float unitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < unitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 150;
                health.Value = 150;
                health.Type = ArmorType.Light;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagMarksman), typeof(TagLegionnaire))]
    struct MarksmanSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 75;
                health.Value = 75;
                health.Type = ArmorType.None;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagKnight), typeof(TagLegionnaire))]
    struct KnightSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 150;
                health.Value = 150;
                health.Type = ArmorType.Medium;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagSpellslinger), typeof(TagLegionnaire))]
    struct SpellslingerSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 50;
                health.Value = 50;
                health.Type = ArmorType.None;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagSiegeEngine), typeof(TagLegionnaire))]
    struct SiegeEngineSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 500;
                health.Value = 500;
                health.Type = ArmorType.Heavy;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagDemolisher), typeof(TagLegionnaire))]
    struct DemolisherSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 600;
                health.Value = 600;
                health.Type = ArmorType.Heavy;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagWyvern), typeof(TagLegionnaire))]
    struct WyvernSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 300;
                health.Value = 300;
                health.Type = ArmorType.None;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagGryphon), typeof(TagLegionnaire))]
    struct GriffinSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 250;
                health.Value = 250;
                health.Type = ArmorType.Light;
                team.Number =2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagBrute), typeof(TagLegionnaire))]
    struct BruteSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 200;
                health.Value = 200;
                health.Type = ArmorType.Light;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagWarlord), typeof(TagLegionnaire))]
    struct WarlordSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 350;
                health.Value = 350;
                health.Type = ArmorType.Heavy;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }
    [RequireComponentTag(typeof(TagWarlock), typeof(TagLegionnaire))]
    struct WarlockSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 100;
                health.Value = 100;
                health.Type = ArmorType.None;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagRaider), typeof(TagLegionnaire))]
    struct RaiderSpawnJob : IJobForEachWithEntity<Translation, Target, Health, Team>
    {
        [ReadOnly] public EntityCommandBuffer CommandBuffer;
        public float UnitCount;

        public void Execute(Entity entity, int index, ref Translation trans, ref Target target, ref Health health, ref Team team)
        {
            if (index < UnitCount)
            {

                trans.Value = float3.zero;
                target.Destination = float3.zero;
                target.Entity = Entity.Null;
                health.Max = 200;
                health.Value = 200;
                health.Type = ArmorType.Light;
                team.Number = 2;
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }




    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var Humanunitamount = GetSingleton<HumanAIUnitSpawnerSingleton>();
        var OrcUnitAmount = GetSingleton<OrcAIUnitSpawnerSingleton>();

        //for the pause menu use Time.timeScale so that we can have the same time in game always.


        //It will normally be 60 seconds, but is 5 for testing

        {
            ///This is the wall of jobs each one is going to check if the amount is
            ///greater than zero and it is going to run through the job to spawn it
            if (Humanunitamount.Legionnaires > 0)
            {
                var LegionnaireJob = new LegionnaireSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    unitCount = Humanunitamount.Legionnaires
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(LegionnaireJob);
                Humanunitamount.Legionnaires = 0;
                SetSingleton<HumanAIUnitSpawnerSingleton>(Humanunitamount);
                LegionnaireJob.Complete();
            }
            ///////////////////////////////////////
            ///

            if (Humanunitamount.Paladins > 0)
            {

                var PaladinJob = new PaladinSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = Humanunitamount.Paladins
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(PaladinJob);
                Humanunitamount.Paladins = 0;
                SetSingleton<HumanAIUnitSpawnerSingleton>(Humanunitamount);

                PaladinJob.Complete();
            }
            /////////////////////////////////////
            ///
            if (OrcUnitAmount.Sluggers > 0)
            {

                var SluggerJob = new SluggerSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = OrcUnitAmount.Sluggers
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(SluggerJob);
                OrcUnitAmount.Sluggers = 0;
                SetSingleton<OrcAIUnitSpawnerSingleton>(OrcUnitAmount);

                SluggerJob.Complete();
            }
            /////////////////////////////////////
            ///
            if (Humanunitamount.Marksmen > 0)
            {

                var MarksmanJob = new MarksmanSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = Humanunitamount.Marksmen
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(MarksmanJob);
                Humanunitamount.Marksmen = 0;
                SetSingleton<HumanAIUnitSpawnerSingleton>(Humanunitamount);
                MarksmanJob.Complete();
            }
            //////////////////////////////////////
            ///
            if (Humanunitamount.Knights > 0)
            {

                var KnightJob = new KnightSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = Humanunitamount.Knights
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(KnightJob);
                Humanunitamount.Knights = 0;
                SetSingleton<HumanAIUnitSpawnerSingleton>(Humanunitamount);

                KnightJob.Complete();
            }
            //////////////////////////////////////
            ///
            if (Humanunitamount.SpellSlingers > 0)
            {

                var SpellSlingerJob = new SpellslingerSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = Humanunitamount.SpellSlingers
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(SpellSlingerJob);
                Humanunitamount.SpellSlingers = 0;
                SetSingleton<HumanAIUnitSpawnerSingleton>(Humanunitamount);
                SpellSlingerJob.Complete();
            }
            /////////////////////////////////////
            ///
            if (Humanunitamount.SiegeEngines > 0)
            {

                var SiegeEngineJob = new SiegeEngineSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = Humanunitamount.SiegeEngines
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(SiegeEngineJob);
                Humanunitamount.SiegeEngines = 0;
                SetSingleton<HumanAIUnitSpawnerSingleton>(Humanunitamount);

                SiegeEngineJob.Complete();
            }
            //////////////////////////////////////
            ///
            if (OrcUnitAmount.Demolishers > 0)
            {

                var DemolisherJob = new DemolisherSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = OrcUnitAmount.Demolishers
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(DemolisherJob);
                OrcUnitAmount.Demolishers = 0;
                SetSingleton<OrcAIUnitSpawnerSingleton>(OrcUnitAmount);

                DemolisherJob.Complete();
            }
            /////////////////////////////////////
            ///
            if (OrcUnitAmount.Wyverns > 0)
            {

                var WyvernJob = new WyvernSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = OrcUnitAmount.Wyverns
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(WyvernJob);
                OrcUnitAmount.Wyverns = 0;
                SetSingleton<OrcAIUnitSpawnerSingleton>(OrcUnitAmount);


                WyvernJob.Complete();
            }
            ///////////////////////////////////////
            ///
            if (Humanunitamount.Griffins > 0)
            {

                var GriffinJob = new GriffinSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = Humanunitamount.Griffins
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(GriffinJob);
                Humanunitamount.Griffins = 0;
                SetSingleton<HumanAIUnitSpawnerSingleton>(Humanunitamount);


                GriffinJob.Complete();
            }
            ////////////////////////////////////
            ///
            if (OrcUnitAmount.Brutes > 0)
            {

                var BruteJob = new BruteSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = OrcUnitAmount.Brutes
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(BruteJob);
                OrcUnitAmount.Brutes = 0;
                SetSingleton<OrcAIUnitSpawnerSingleton>(OrcUnitAmount);


                BruteJob.Complete();
            }
            if (OrcUnitAmount.Warlords > 0)
            {

                var WarlordJob = new WarlordSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = OrcUnitAmount.Warlords
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(WarlordJob);
                OrcUnitAmount.Warlords = 0;
                SetSingleton<OrcAIUnitSpawnerSingleton>(OrcUnitAmount);


                WarlordJob.Complete();
            }
            if (OrcUnitAmount.Warlocks > 0)
            {

                var WarlockJob = new WarlockSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = OrcUnitAmount.Warlocks
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(WarlockJob);
                OrcUnitAmount.Warlocks = 0;
                SetSingleton<OrcAIUnitSpawnerSingleton>(OrcUnitAmount);


                WarlockJob.Complete();
            }
            if (OrcUnitAmount.Raiders > 0)
            {

                var RaiderJob = new RaiderSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = OrcUnitAmount.Raiders
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(RaiderJob);
                OrcUnitAmount.Raiders = 0;
                SetSingleton<OrcAIUnitSpawnerSingleton>(OrcUnitAmount);


                RaiderJob.Complete();
            }
        }
        return inputDeps;
    }

}

