using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

public class SetTargetDestinationSystem : JobComponentSystem
{
    [RequireComponentTag(typeof(TagSelected))]
    struct MoveFormationJob : IJobForEachWithEntity<Target, Translation, Selectable>
    {
        public NativeArray<Translation> unitPositions;
        public float3 initialMouseRaycastPosition;
        public float3 currentMouseRaycastPosition;
        public int unitLength;

        public void Execute(Entity entity, int i, ref Target target, ref Translation position, ref Selectable selectable)
        {
            target.Action = UnitAction.Move;
            target.Destination = initialMouseRaycastPosition + CalculateFormations(initialMouseRaycastPosition.y, i, selectable.SelectSize);
        }

        public float3 CalculateFormations(float height, int index, float unitSize)
        {
            int maxPerRow = (int)math.ceil((math.distance(initialMouseRaycastPosition, currentMouseRaycastPosition) / 15));
            int rowCount = (int)math.ceil(unitLength / maxPerRow);
            int rowIndex = (int)math.ceil(index / maxPerRow);
            int positionInRow = index % maxPerRow;
            int rowLength = unitLength - rowIndex * maxPerRow;
            rowLength = math.clamp(rowLength, 1, maxPerRow);

            float positionX = (positionInRow * unitSize) - (maxPerRow / 2 * unitSize) + ((maxPerRow - rowLength) / 2 * unitSize) + (rowLength) + (rowLength % 2 == 0 ? 0 : unitSize / 2);
            float positionZ = (rowCount / 2 - rowIndex) * unitSize;

            return new float3(positionX, height, positionZ);
        }
    };

    [RequireComponentTag(typeof(TagSelected))]
    struct OffsetJob : IJobForEachWithEntity<Target, Translation>
    {
        [ReadOnly] public NativeArray<Translation> unitPositions;
        [ReadOnly] public float3 initialMouseRaycastPosition;

        public void Execute(Entity entity, int i, ref Target target, ref Translation position)
        {
            target.Action = UnitAction.Move;
            var centerOffset = CalculateOffset(unitPositions);
            target.Destination = centerOffset.Equals(float3.zero) ? initialMouseRaycastPosition : initialMouseRaycastPosition + (position.Value - centerOffset);
        }

        public float3 CalculateOffset(NativeArray<Translation> unitPos)
        {
            var offset = float3.zero;

            if (unitPos.Length > 1)
            {
                for (int i = 0; i < unitPos.Length; i++)
                {
                    offset += unitPos[i].Value;
                }
                offset /= unitPos.Length;
            }

            return offset;
        }
    };

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();

        var jobHandle = inputDeps;

        if (mouse.RightClickDown)
        {
            EntityQuery m_SelectedUnitPositions = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<TagSelected>());

            NativeArray<Translation> m_UnitPositions = m_SelectedUnitPositions.ToComponentDataArray<Translation>(Allocator.TempJob);

            if (math.distance(mouse.InitialMouseRaycastPosition, mouse.CurrentMouseRaycastPosition) >= 15)
            {
                var formationJob = new MoveFormationJob
                {
                    currentMouseRaycastPosition = mouse.CurrentMouseRaycastPosition,
                    initialMouseRaycastPosition = mouse.InitialMouseRaycastPosition,
                    unitPositions = m_UnitPositions,
                    unitLength = m_UnitPositions.Length
                };

                jobHandle = formationJob.ScheduleSingle(this, inputDeps);

                jobHandle.Complete();
            }
            else if (math.distance(mouse.InitialMouseRaycastPosition, mouse.CurrentMouseRaycastPosition) < 15)
            {
                var offsetJob = new OffsetJob
                {
                    initialMouseRaycastPosition = mouse.InitialMouseRaycastPosition,
                    unitPositions = m_UnitPositions
                };

                jobHandle = offsetJob.Schedule(this, inputDeps);

                jobHandle.Complete();
            }

            m_UnitPositions.Dispose();
        }

        return jobHandle;
    }
}
