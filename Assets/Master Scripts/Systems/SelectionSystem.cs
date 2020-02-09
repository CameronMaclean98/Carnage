using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;

// TODO: Implement double click feature to select all units of that same type within the entire screen.

[UpdateAfter(typeof(MouseInputSystem))]
public class SelectionSystem : JobComponentSystem
{
    BeginSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    struct SelectClickJob : IJobForEachWithEntity<Translation, Selectable>
    {
        [ReadOnly] public float3 currentMouseRayPosition;
        [ReadOnly] public ComponentDataFromEntity<TagSelected> selected;
        [ReadOnly] public EntityCommandBuffer entityCommandBuffer;


        public void Execute(Entity entity, int index, ref Translation position, ref Selectable selectable)
        {
            if ( math.distance(currentMouseRayPosition, position.Value) <= selectable.SelectSize )
            {
                if ( !selected.Exists(entity) )
                {
                    Debug.Log("I am selected");
                    entityCommandBuffer.AddComponent(entity, new TagSelected { });
                }
            }
            else
            {
                if ( selected.Exists(entity) )
                    entityCommandBuffer.RemoveComponent<TagSelected>(entity);
            }
        }
    }

    struct SelectionBoxJob : IJobForEachWithEntity<Translation, Selectable>
    {
        [ReadOnly] public float3 currentMouseRayPosition;
        [ReadOnly] public float3 initialMouseRayPosition;
        [ReadOnly] public ComponentDataFromEntity<TagSelected> selected;
        [ReadOnly] public EntityCommandBuffer entityCommandBuffer;

        public void Execute(Entity entity, int i, [ReadOnly] ref Translation position, [ReadOnly] ref Selectable selectable)
        {
            if ( math.distance(initialMouseRayPosition, currentMouseRayPosition) > selectable.SelectSize )
            {
                float3 selectionMinRect = MinFloat3(initialMouseRayPosition, currentMouseRayPosition);
                float3 selectionMaxRect = MaxFloat3(initialMouseRayPosition, currentMouseRayPosition);
                if ( IsUnitInSelection(position.Value, selectionMinRect, selectionMaxRect))
                {
                    entityCommandBuffer.AddComponent(entity, new TagSelected { });
                }
            }
        }

        public static bool IsUnitInSelection(float3 unitPosition, float3 minRect, float3 maxRect)
        {
            bool xPos = false;
            bool yPos = false;
            bool zPos = false;

            if ( unitPosition.x >= minRect.x && unitPosition.x <= maxRect.x )
                xPos = true;
            if ( unitPosition.y >= minRect.y && unitPosition.y <= maxRect.y )
                yPos = true;
            if ( unitPosition.z >= minRect.z && unitPosition.z <= maxRect.z )
                zPos = true;

            return (xPos && yPos && zPos);
        }

        public static float3 MinFloat3(float3 position1, float3 position2)
        {
            float x, z;

            x = position1.x <= position2.x ? position1.x : position2.x;
            z = position1.z <= position2.z ? position1.z : position2.z;

            return new float3(x, -200, z);
        }

        public static float3 MaxFloat3(float3 position1, float3 position2)
        {
            float x, z;

            x = position1.x >= position2.x ? position1.x : position2.x;
            z = position1.z >= position2.z ? position1.z : position2.z;

            return new float3(x, 200, z);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouse = GetSingleton<SingletonMouseInput>();

        var jobHandle = inputDeps;

        if ( mouse.LeftClickUp )
        {
            var selectionBoxJob = new SelectionBoxJob
            {
                currentMouseRayPosition = mouse.CurrentMouseRaycastPosition,
                initialMouseRayPosition = mouse.InitialMouseRaycastPosition,
                selected = GetComponentDataFromEntity<TagSelected>(),
                entityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            };

            jobHandle = selectionBoxJob.ScheduleSingle(this, inputDeps);
        }
        else if ( mouse.LeftClickDown )
        {
            var selectClickJob = new SelectClickJob
            {
                currentMouseRayPosition = mouse.CurrentMouseRaycastPosition,
                selected = GetComponentDataFromEntity<TagSelected>(),
                entityCommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer(),
            };

            jobHandle = selectClickJob.Schedule(this, inputDeps);
        }

        m_EntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

        return jobHandle;
    }
}