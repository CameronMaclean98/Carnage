using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;

public class SetTargetInRangeSystem : JobComponentSystem
{
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    struct SetTargetInRangeJob : IJobForEachWithEntity<Range, Target, Translation>
    {

        [ReadOnly] public EntityCommandBuffer entityCommandBuffer;
        [ReadOnly] public ComponentDataFromEntity<TagTargetInRange> targetInRange;

        public void Execute(Entity entity, int index, [ReadOnly] ref Range range, [ReadOnly] ref Target target, [ReadOnly] ref Translation position)
        {
            float3 targetPosition = target.Destination;

            // Check if target's position is within the entity's range
            if (math.distance(position.Value, targetPosition) <= range.Value)
            {
                entityCommandBuffer.AddComponent(entity, new TagTargetInRange { });
            }
            else
            {
                // remove TagTargetInRange (if it is added) when the target is not in range
                if (targetInRange.Exists(entity))
                {
                    entityCommandBuffer.RemoveComponent<TagTargetInRange>(entity);
                }
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var targetInRangeJob = new SetTargetInRangeJob
        {
            entityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            targetInRange = GetComponentDataFromEntity<TagTargetInRange>(),

        }.ScheduleSingle(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(targetInRangeJob);

        return targetInRangeJob;
    }
}
