using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using System;

/* 
                    None     |    Light         Medium       Heavy        Fortified
    Piercing,       150%     |    75 %     |    100 %     |   150%      |  35  %
    Slashing,       100%     |    150%     |    100 %     |   100%      |  50  %
    Bludgeoning,    150%     |    75 %     |    100 %     |   200%      |  75  %
    Magical,        100%     |    100%     |    200 %     |   100%      |  50  %
    Artillery       150%     |    50 %     |    100 %     |   100%      |  150 %

    subject to change...
*/

public class DamageSystem : JobComponentSystem
{

    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagMeleeUnit))]
    struct DoMeleeJob : IJobForEachWithEntity<Translation,Damage, Target, Rate, Range>
    {
        [ReadOnly] public EntityCommandBuffer entityCommandBuffer;
        [ReadOnly] public NativeArray<Entity> units;
        [ReadOnly] public NativeArray<Health> unitsHealth;
        [ReadOnly] public uint seed;

        public void Execute(Entity entity, int index, ref Translation pos, ref Damage damage, ref Target target, ref Rate rate, ref Range range)
        {
            //if unit is ready to attack
            if(target.Action == UnitAction.Attack && rate.Cooldown == 1)
            {
                //reset rate values
                rate.Cooldown = 0f;
                rate.Value = 0f;

                var random = new Unity.Mathematics.Random(seed);
                var baseDamage = random.NextFloat(damage.Min, damage.Max);
                
                //do attack animation
                for(int i = 0; i < units.Length; i++)
                {
                    //if a unit from the native array matches the units target entity
                    if(units[i] == target.Entity)
                    {
                        //calculate damage using table above **Note Random.Range does not work in jobs. Need to find alt solution**
                        float actualDamage = 0;
                        switch(damage.Type)
                        {
                            case AttackType.Piercing:
                                switch (unitsHealth[i].Type)
                                {
                                    case ArmorType.None:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                    case ArmorType.Light:
                                        actualDamage = baseDamage * 0.75f;
                                        break;
                                    case ArmorType.Medium:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Heavy:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                    case ArmorType.Fortified:
                                        actualDamage = baseDamage * 0.35f;
                                        break;
                                }
                                break;
                            case AttackType.Slashing:
                                switch (unitsHealth[i].Type)
                                {
                                    case ArmorType.None:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Light:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                    case ArmorType.Medium:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Heavy:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Fortified:
                                        actualDamage = baseDamage * 0.5f;
                                        break;
                                }
                                break;
                            case AttackType.Bludgeoning:
                                switch (unitsHealth[i].Type)
                                {
                                    case ArmorType.None:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                    case ArmorType.Light:
                                        actualDamage = baseDamage * 0.75f;
                                        break;
                                    case ArmorType.Medium:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Heavy:
                                        actualDamage = baseDamage * 2.0f;
                                        break;
                                    case ArmorType.Fortified:
                                        actualDamage = baseDamage * 0.75f;
                                        break;
                                }
                                break;
                            case AttackType.Magical:
                                switch (unitsHealth[i].Type)
                                {
                                    case ArmorType.None:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Light:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Medium:
                                        actualDamage = baseDamage * 2.0f;
                                        break;
                                    case ArmorType.Heavy:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Fortified:
                                        actualDamage = baseDamage * 0.5f;
                                        break;
                                }
                                break;
                            case AttackType.Artillery:
                                switch (unitsHealth[i].Type)
                                {
                                    case ArmorType.None:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                    case ArmorType.Light:
                                        actualDamage = baseDamage * 0.5f;
                                        break;
                                    case ArmorType.Medium:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Heavy:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Fortified:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                }
                                break;

                        }
                        
                        //Set the new health value of unit that was attacked
                        var newHealth = unitsHealth[i];
                        newHealth.Value -= actualDamage;
                        entityCommandBuffer.SetComponent<Health>(units[i], newHealth);
                    }
                }
            }
        }
    }

    [RequireComponentTag(typeof(TagRangeUnit))]
    struct DoRangeJob : IJobForEachWithEntity<Translation, Damage, Target, Rate, Range, SpawnUnit>
    {
        [ReadOnly]
        public EntityCommandBuffer entityCommandBuffer;
        [ReadOnly]
        public NativeArray<Entity> units;
        [ReadOnly]
        public NativeArray<Health> unitsHealth;

        public void Execute(Entity entity, int index, ref Translation pos, ref Damage damage, ref Target target, ref Rate rate, ref Range range,ref SpawnUnit spawn)
        {
            //if unit is ready to attack
            if (target.Action == UnitAction.Attack && rate.Cooldown == 1)
            {
                //reset rate values
                rate.Cooldown = 0f;
                rate.Value = 0f;

                //do attack animation
                for (int i = 0; i < units.Length; i++)
                {
                    //if a unit from the native array matches the units target entity
                    if (units[i] == target.Entity)
                    {
                        //create projectile and pass units data to it
                        var instance = entityCommandBuffer.Instantiate(spawn.Prefab);

                        entityCommandBuffer.SetComponent(instance, new Translation { Value = pos.Value });
                        entityCommandBuffer.AddComponent(instance, new Target { Destination = target.Destination, Action = UnitAction.Move, Entity = target.Entity });
                        entityCommandBuffer.AddComponent(instance, new MovementSpeed { Value = 5.0f });
                        entityCommandBuffer.AddComponent(instance, new Damage { Max = damage.Max, Min = damage.Min, Type = AttackType.Piercing });
                        entityCommandBuffer.AddComponent(instance, new Range { MaxEngagement = 50f, Max = 2f, Min = 0.5f, Value = 1f });
                        entityCommandBuffer.AddComponent(instance, new Rate { Cooldown = 1f, Max = 5f, Time = 0f, Value = 5f });
                        entityCommandBuffer.AddComponent(instance, new TagProjectile());
                        entityCommandBuffer.AddComponent(instance, new TagUnit()); //need this for inRange system to work
                    }
                }
            }
        }
    }

    [RequireComponentTag(typeof(TagProjectile))]
    struct DoProjectileJob : IJobForEachWithEntity<Translation, Damage, Target, Rate, Range>
    {
        [ReadOnly]
        public EntityCommandBuffer entityCommandBuffer;
        [ReadOnly]
        public NativeArray<Entity> units;
        [ReadOnly]
        public NativeArray<Health> unitsHealth;
        [ReadOnly]
        public uint seed;

        public void Execute(Entity entity, int index, ref Translation pos, ref Damage damage, ref Target target, ref Rate rate, ref Range range)
        {
            //if projectile has reacted its target. If unit action is attack, then distance is in range. Can see how it works with InRange script.
            if (target.Action == UnitAction.Attack && rate.Cooldown == 1)
            {
                //reset rate values so it doesnt do damage more than once
                rate.Cooldown = 0f;
                rate.Value = 0f;

                //random damage between min and max
                var random = new Unity.Mathematics.Random(seed);
                var baseDamage = random.NextFloat(damage.Min, damage.Max);

                //do attack animation

                for (int i = 0; i < units.Length; i++)
                {
                    //if a unit from the native array matches the units target entity
                    if (units[i] == target.Entity)
                    {
                        float actualDamage = 0;
                        switch (damage.Type)
                        {
                            case AttackType.Piercing:
                                switch (unitsHealth[i].Type)
                                {
                                    case ArmorType.None:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                    case ArmorType.Light:
                                        actualDamage = baseDamage * 0.75f;
                                        break;
                                    case ArmorType.Medium:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Heavy:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                    case ArmorType.Fortified:
                                        actualDamage = baseDamage * 0.35f;
                                        break;
                                }
                                break;
                            case AttackType.Slashing:
                                switch (unitsHealth[i].Type)
                                {
                                    case ArmorType.None:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Light:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                    case ArmorType.Medium:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Heavy:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Fortified:
                                        actualDamage = baseDamage * 0.5f;
                                        break;
                                }
                                break;
                            case AttackType.Bludgeoning:
                                switch (unitsHealth[i].Type)
                                {
                                    case ArmorType.None:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                    case ArmorType.Light:
                                        actualDamage = baseDamage * 0.75f;
                                        break;
                                    case ArmorType.Medium:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Heavy:
                                        actualDamage = baseDamage * 2.0f;
                                        break;
                                    case ArmorType.Fortified:
                                        actualDamage = baseDamage * 0.75f;
                                        break;
                                }
                                break;
                            case AttackType.Magical:
                                switch (unitsHealth[i].Type)
                                {
                                    case ArmorType.None:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Light:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Medium:
                                        actualDamage = baseDamage * 2.0f;
                                        break;
                                    case ArmorType.Heavy:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Fortified:
                                        actualDamage = baseDamage * 0.5f;
                                        break;
                                }
                                break;
                            case AttackType.Artillery:
                                switch (unitsHealth[i].Type)
                                {
                                    case ArmorType.None:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                    case ArmorType.Light:
                                        actualDamage = baseDamage * 0.5f;
                                        break;
                                    case ArmorType.Medium:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Heavy:
                                        actualDamage = baseDamage * 1.0f;
                                        break;
                                    case ArmorType.Fortified:
                                        actualDamage = baseDamage * 1.5f;
                                        break;
                                }
                                break;

                        }

                        //Set the new health value of unit that was attacked
                        var newHealth = unitsHealth[i];
                        newHealth.Value -= actualDamage;
                        entityCommandBuffer.SetComponent<Health>(units[i], newHealth);
                    }
                }
                //destroy projectile if it hit
                entityCommandBuffer.DestroyEntity(entity); 
            }
            //if projectile missed target, destroy it once it reaches its destination. would happen if target is moving, and arrows
            //attack destination is meet
            if(entity != Entity.Null)
            {
                if(math.distance(pos.Value, target.Destination) <= 0.5f)
                {
                    entityCommandBuffer.DestroyEntity(entity);
                }
            }
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //Get all entitys that have health
        EntityQuery m_attackUnits = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<Health>());

        //Array of entities from query
        NativeArray<Entity> m_units = m_attackUnits.ToEntityArray(Allocator.TempJob);

        //Array of health components matching the entites above
        NativeArray<Health> m_unitHealth = m_attackUnits.ToComponentDataArray<Health>(Allocator.TempJob);

        var job1 = new DoMeleeJob
        {
            entityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            units = m_units,
            unitsHealth = m_unitHealth,
            seed = (uint)UnityEngine.Random.Range(1, 10000)
        };
        
        //schedule and wait for completion
        JobHandle handle = job1.ScheduleSingle(this,inputDeps);
        handle.Complete();

        var job2 = new DoRangeJob
        {
            entityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            units = m_units,
            unitsHealth = m_unitHealth
        };

        //schedule and wait for completion
        handle = job2.ScheduleSingle(this, inputDeps);
        handle.Complete();

        var job3 = new DoProjectileJob
        {
            entityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            units = m_units,
            unitsHealth = m_unitHealth,
            seed = (uint)UnityEngine.Random.Range(1, 10000)
        };

        //schedule and wait for completion
        handle = job3.ScheduleSingle(this, inputDeps);
        handle.Complete();

        //clean up
        m_unitHealth.Dispose();
        m_units.Dispose();

        return handle;
    }
}
