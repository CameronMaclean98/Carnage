//system that regen health to a player if there is no entities within range
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms;
using Unity.Collections;


public class HealthRegenerationSystem : JobComponentSystem
{
    [BurstCompile]
    struct HealthRegenJob : IJobForEach<Health, HealthRegeneration, Translation, Team>
    {
        public NativeArray<Translation> EnemiesPosition;
        public NativeArray<Team> UnitTeam;
        public float time;
        public void Execute(ref Health health, ref HealthRegeneration healthRegen, ref Translation position, ref Team team)
        {

            //here we need to check if enemies within the range
            healthRegen.Time += time;
            bool Can_heal = true;

            for (int i = 0; i < EnemiesPosition.Length; i++)
            {
                var distance = math.distance(position.Value, EnemiesPosition[i].Value);

                    if (team.Number != UnitTeam[i].Number && distance < 5)  //if the entity is an enemy and it's distance < 5 units player cant heal
                {
                        Can_heal = false;
                        break;

                    }
                


            }
            if (Can_heal == true && healthRegen.Time >= healthRegen.Reset)
            {
                healthRegen.Time = 0f; //reset health regen time
                health.Value += healthRegen.Value; // regenerate "value" points every second 

                if (health.Value > healthRegen.Max)
                {
                    health.Value = healthRegen.Max;
                }
            }
        }



    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = inputDeps;

        EntityQuery m_LiveEnemiesPositions = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<Team>());
        NativeArray<Translation> m_EnemiesPositions = m_LiveEnemiesPositions.ToComponentDataArray<Translation>(Allocator.Persistent);
        NativeArray<Team> m_team = m_LiveEnemiesPositions.ToComponentDataArray<Team>(Allocator.TempJob);

        var healthRegenJob = new HealthRegenJob
        {
            EnemiesPosition = m_EnemiesPositions,
            UnitTeam = m_team,
            time = Time.deltaTime

        };
        jobHandle = healthRegenJob.ScheduleSingle(this, inputDeps);
        jobHandle.Complete();

        m_EnemiesPositions.Dispose();
        return inputDeps;
    }

}

