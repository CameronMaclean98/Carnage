using Unity.Jobs;
using Unity.Collections;
using Unity.Entities;

public class HealthSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    [RequireComponentTag(typeof(TagUnit))]
    [ExcludeComponent(typeof(TagSiegeEngine), typeof(TagDemolisher))]
    struct CheckUnitHealthJob : IJobForEachWithEntity<Health>
    {
        public EntityCommandBuffer CommandBuffer;

        public void Execute(Entity entity, int index, [ReadOnly] ref Health health)
        {
            if (health.Value < 0)
            {
                CommandBuffer.DestroyEntity(entity);
            }
            else if (health.Value <= health.Max)
            {
               CommandBuffer.AddComponent(entity, new HealthRegeneration { Time = 5f, Max = 100f, Reset = 5f, Value = 5f});
            }
        }
    }

    [RequireComponentTag(typeof(TagSiegeEngine))]
    struct CheckSiegeEngineHealthJob : IJobForEachWithEntity<Health>
    {
        public EntityCommandBuffer CommandBuffer;

        public void Execute(Entity entity, int index, [ReadOnly] ref Health health)
        {
            if (health.Value < 0)
            {
                CommandBuffer.DestroyEntity(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagDemolisher))]
    struct CheckDemolisherHealthJob : IJobForEachWithEntity<Health>
    {
        public EntityCommandBuffer CommandBuffer;

        public void Execute(Entity entity, int index, [ReadOnly] ref Health health)
        {
            if (health.Value < 0)
            {
                CommandBuffer.DestroyEntity(entity);
            }
        }
    }

    [RequireComponentTag(typeof(TagBuilding))]
    struct CheckBuildingHealthJob : IJobForEachWithEntity<Health>
    {
        public EntityCommandBuffer CommandBuffer;

        public void Execute(Entity entity, int index, [ReadOnly] ref Health health)
        {
            if (health.Value < 0)
            {
                CommandBuffer.DestroyEntity(entity);
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = inputDeps;

        var checkUnitHealthJob = new CheckUnitHealthJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
        }.ScheduleSingle(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(checkUnitHealthJob);
        checkUnitHealthJob.Complete();

        var checkSiegeEngineHealthJob = new CheckSiegeEngineHealthJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
        }.ScheduleSingle(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(checkSiegeEngineHealthJob);
        checkSiegeEngineHealthJob.Complete();

        var checkDemolisherHealthJob = new CheckDemolisherHealthJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
        }.ScheduleSingle(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(checkDemolisherHealthJob);
        checkDemolisherHealthJob.Complete();


        var checkBuildingHealthJob = new CheckBuildingHealthJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
        }.ScheduleSingle(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(checkBuildingHealthJob);
        checkBuildingHealthJob.Complete();


        return jobHandle;
    }
}