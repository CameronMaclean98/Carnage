
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(KeyboardInputSystem))]

public class SpawnBarrier : EntityCommandBufferSystem { }

// This entire code has been Edited by Cameron Maclean. It is a mess, but it works so it is fine for now woohoo
public class UnitSpawnSystem : JobComponentSystem
{
    // BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    SpawnBarrier m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
        // m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        m_EntityCommandBufferSystem = World.GetExistingSystem<SpawnBarrier>();

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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
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
                team.Number = 1;
                CommandBuffer.AddComponent(entity, new Selectable { SelectSize = 5.0f });
                CommandBuffer.RemoveComponent<TagUnitPool>(entity);
            }
        }
    }




    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var unitamount = GetSingleton<UnitSpawnerSingleton>();
        unitamount.spawntime += Time.deltaTime;
        SetSingleton<UnitSpawnerSingleton>(unitamount);

        //for the pause menu use Time.timeScale so that we can have the same time in game always.


        //It will normally be 60 seconds, but is 5 for testing

        if ((int)unitamount.spawntime >= 60)
        {
            unitamount.spawntime = 0;
            SetSingleton<UnitSpawnerSingleton>(unitamount);
            ///This is the wall of jobs each one is going to check if the amount is
            ///greater than zero and it is going to run through the job to spawn it
            if (unitamount.Legionnaires > 0)
            {
                var LegionnaireJob = new LegionnaireSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    unitCount = unitamount.Legionnaires
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(LegionnaireJob);
                unitamount.Legionnaires = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);

                LegionnaireJob.Complete();
            }
            ///////////////////////////////////////
            ///

            if (unitamount.Paladins > 0)
            {

                var PaladinJob = new PaladinSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Paladins
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(PaladinJob);
                unitamount.Paladins = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);

                PaladinJob.Complete();
            }
            /////////////////////////////////////
            ///
            if (unitamount.Sluggers > 0)
            {

                var SluggerJob = new SluggerSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Marksmen
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(SluggerJob);
                unitamount.Sluggers = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);

                SluggerJob.Complete();
            }
            /////////////////////////////////////
            ///
            if (unitamount.Marksmen > 0)
            {

                var MarksmanJob = new MarksmanSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Marksmen
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(MarksmanJob);
                unitamount.Marksmen = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);
                MarksmanJob.Complete();
            }
            //////////////////////////////////////
            ///
            if (unitamount.Knights > 0)
            {

                var KnightJob = new KnightSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Knights
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(KnightJob);
                unitamount.Knights = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);

                KnightJob.Complete();
            }
            //////////////////////////////////////
            ///
            if (unitamount.SpellSlingers > 0)
            {

                var SpellSlingerJob = new SpellslingerSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.SpellSlingers
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(SpellSlingerJob);
                unitamount.SpellSlingers = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);
                SpellSlingerJob.Complete();
            }
            /////////////////////////////////////
            ///
            if (unitamount.SiegeEngines > 0)
            {

                var SiegeEngineJob = new SiegeEngineSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.SiegeEngines
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(SiegeEngineJob);
                unitamount.SiegeEngines = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);

                SiegeEngineJob.Complete();
            }
            //////////////////////////////////////
            ///
            if (unitamount.Demolishers > 0)
            {

                var DemolisherJob = new DemolisherSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Demolishers
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(DemolisherJob);
                unitamount.Demolishers = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);

                DemolisherJob.Complete();
            }
            /////////////////////////////////////
            ///
            if (unitamount.Wyverns > 0)
            {

                var WyvernJob = new WyvernSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Wyverns
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(WyvernJob);
                unitamount.Wyverns = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);


                WyvernJob.Complete();
            }
            ///////////////////////////////////////
            ///
            if (unitamount.Griffins > 0)
            {

                var GriffinJob = new GriffinSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Griffins
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(GriffinJob);
                unitamount.Griffins = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);


                GriffinJob.Complete();
            }
            ////////////////////////////////////
            ///
            if (unitamount.Brutes > 0)
            {

                var BruteJob = new BruteSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Brutes
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(BruteJob);
                unitamount.Brutes = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);


                BruteJob.Complete();
            }
            if (unitamount.Warlords > 0)
            {

                var WarlordJob = new WarlordSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Warlords
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(WarlordJob);
                unitamount.Warlords = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);


                WarlordJob.Complete();
            }
            if (unitamount.Warlocks > 0)
            {

                var WarlockJob = new WarlockSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Warlocks
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(WarlockJob);
                unitamount.Warlocks = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);


                WarlockJob.Complete();
            }
            if (unitamount.Raiders > 0)
            {

                var RaiderJob = new RaiderSpawnJob
                {
                    CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
                    UnitCount = unitamount.Raiders
                }.ScheduleSingle(this, inputDeps);

                m_EntityCommandBufferSystem.AddJobHandleForProducer(RaiderJob);
                unitamount.Raiders = 0;
                SetSingleton<UnitSpawnerSingleton>(unitamount);


                RaiderJob.Complete();
            }
        }
        return inputDeps;
    }

}

