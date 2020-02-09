using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;

[UpdateAfter(typeof(FermundSpellCastSystem))]
public class FermundsHealingWardSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagHealingWard))]
    struct HealingWardLocation : IJobForEachWithEntity<Translation, Ward>
    {
        public NativeArray<float3> wardLocation;
        public EntityCommandBuffer CommandBuffer;
        public float DeltaTime;
        public void Execute(Entity entity, int index, ref Translation location, ref Ward ward)
        {
            wardLocation[0] = location.Value;
            if (ward.Duration > 0f)
            {
                ward.Duration -= 1 * DeltaTime;
            }
            if (ward.Duration <= 0f)
            {
                CommandBuffer.RemoveComponent<TagHealingWard>(entity);
                CommandBuffer.RemoveComponent<Ward>(entity);
                CommandBuffer.DestroyEntity(entity);
                //CommandBuffer.AddComponent(entity, new TagDestroy { });
            }
        }
    }

    [RequireComponentTag(typeof(TagUnit))]
    [ExcludeComponent(typeof(TagEnemyUnit))]
    struct LegionnaireLocationJob : IJobForEachWithEntity<Translation, Health, ShaderComponent>
    {

        public NativeArray<float3> wardLocation;
        public float DeltaTime;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health, ref ShaderComponent shader)
        {

            var wardDistance = math.distance(location.Value, wardLocation[0]);
            var maxHealth = health.Max;
            var currentHealth = health.Value;
            if (wardDistance <= 30f && currentHealth <= maxHealth)
            {
                shader.change = true;
                shader.HealBuff = 1;
                Debug.Log("Healing Legionnaire");
                health.Value += 10f * DeltaTime;
            }
            else
            {
                shader.change = true;
                shader.HealBuff = 0;

            }

        }
    }
    /*
     * This will now be affecting all units and no longer a particular side
    [RequireComponentTag(typeof(TagMarksman))]
    [ExcludeComponent(typeof(TagEnemyUnit))]
    struct MarksManLocationJob : IJobForEachWithEntity<Translation, Health, ShaderComponent>
    {

        public NativeArray<float3> wardLocation;
        public float DeltaTime;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health, ref ShaderComponent shader)
        {

            var wardDistance = math.distance(location.Value, wardLocation[0]);
            var maxHealth = health.Max;
            var currentHealth = health.Value;
            if (wardDistance <= 30f && currentHealth <= maxHealth)
            {
                shader.change = true;
                shader.HealBuff = 1;
                Debug.Log("Healing Marksman");
                health.Value += 10f * DeltaTime;
            }
            else
            {

                shader.change = true;
                shader.HealBuff = 0;
            }

        }
    }


    [RequireComponentTag(typeof(TagPaladin))]
    [ExcludeComponent(typeof(TagEnemyUnit))]
    struct PaladinLocationJob : IJobForEachWithEntity<Translation, Health, ShaderComponent>
    {

        public NativeArray<float3> wardLocation;
        public float DeltaTime;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health, ref ShaderComponent shader)
        {

            var wardDistance = math.distance(location.Value, wardLocation[0]);
            var maxHealth = health.Max;
            var currentHealth = health.Value;
            if (wardDistance <= 30f && currentHealth <= maxHealth)
            {
                shader.change = true;
                shader.HealBuff = 1;
                Debug.Log("Healing Paladin");
                health.Value += 10f * DeltaTime;
            }
            else
            {
                shader.change = true;
                shader.HealBuff = 0;

            }

        }
    }


    [RequireComponentTag(typeof(TagKnight))]
    [ExcludeComponent(typeof(TagEnemyUnit))]
    struct KnightsLocationJob : IJobForEachWithEntity<Translation, Health, ShaderComponent>
    {
       
        public NativeArray<float3> wardLocation;
        public float DeltaTime;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health, ref ShaderComponent shader)
        {

            var wardDistance = math.distance(location.Value, wardLocation[0]);
            var maxHealth = health.Max;
            var currentHealth = health.Value;
            if(wardDistance <= 30f && currentHealth <= maxHealth)
            {
                shader.change = true;
                shader.HealBuff = 1;
                Debug.Log("Healing knight");
                health.Value += 10f*DeltaTime;
            }
            else
            {
                shader.change = true;
                shader.HealBuff = 0;

            }

        }
    }

    [RequireComponentTag(typeof(TagSpellslinger))]
    [ExcludeComponent(typeof(TagEnemyUnit))]
    struct SpellSlingerLocationJob : IJobForEachWithEntity<Translation, Health>
    {

        public NativeArray<float3> wardLocation;
        public float DeltaTime;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {

            var wardDistance = math.distance(location.Value, wardLocation[0]);
            var maxHealth = health.Max;
            var currentHealth = health.Value;
            if (wardDistance <= 30f && currentHealth <= maxHealth)
            {
                Debug.Log("Healing Spellslinger");
                health.Value += 10f * DeltaTime;
            }
            else
            {


            }

        }
    }

    [RequireComponentTag(typeof(TagGryphon))]
    [ExcludeComponent(typeof(TagEnemyUnit))]
    struct GryphonLocationJob : IJobForEachWithEntity<Translation, Health>
    {

        public NativeArray<float3> wardLocation;
        public float DeltaTime;
        public void Execute(Entity entity, int index, ref Translation location, ref Health health)
        {

            var wardDistance = math.distance(location.Value, wardLocation[0]);
            var maxHealth = health.Max;
            var currentHealth = health.Value;
            if (wardDistance <= 30f && currentHealth <= maxHealth)
            {
                Debug.Log("Healing Gryphon");
                health.Value += 10f * DeltaTime;
            }
            else
            {


            }

        }
    }


    */

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        NativeArray<float3> WardLocation = new NativeArray<float3>(1, Allocator.TempJob);

        var wardJob = new HealingWardLocation
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            wardLocation = WardLocation,
            DeltaTime = Time.deltaTime
        }.ScheduleSingle(this, inputDeps);
        wardJob.Complete();

        if (WardLocation[0].x != 0 && WardLocation[0].y != 0 && WardLocation[0].z != 0)
        {
            Debug.Log("healing ward created");
            var legionnaireToHeal = new LegionnaireLocationJob
            {
                wardLocation = WardLocation,
                DeltaTime = Time.deltaTime
            }.ScheduleSingle(this, inputDeps);
            legionnaireToHeal.Complete();
            /*
            var marksmanToHeal = new PaladinLocationJob
            {
                wardLocation = WardLocation,
                DeltaTime = Time.deltaTime
            }.ScheduleSingle(this, inputDeps);
            marksmanToHeal.Complete();

            var paladinsToHeal = new PaladinLocationJob
            {
                wardLocation = WardLocation,
                DeltaTime = Time.deltaTime
            }.ScheduleSingle(this, inputDeps);
            paladinsToHeal.Complete();

            var knightsToHeal = new KnightsLocationJob
            {
                wardLocation = WardLocation,
                DeltaTime = Time.deltaTime
            }.ScheduleSingle(this, inputDeps);
            knightsToHeal.Complete();

            var spellslingerToHeal = new PaladinLocationJob
            {
                wardLocation = WardLocation,
                DeltaTime = Time.deltaTime
            }.ScheduleSingle(this, inputDeps);
            spellslingerToHeal.Complete();

            var gryphonsToHeal = new PaladinLocationJob
            {
                wardLocation = WardLocation,
                DeltaTime = Time.deltaTime
            }.ScheduleSingle(this, inputDeps);
            gryphonsToHeal.Complete();

   */
        }
     
        WardLocation.Dispose();

        return inputDeps;
    }
}
