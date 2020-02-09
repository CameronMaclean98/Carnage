using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class InRange : JobComponentSystem
{


    [RequireComponentTag(typeof(TagUnit))]
    struct UnitInRangeJob : IJobForEachWithEntity<Translation, Range, Target>
    {
        [ReadOnly] public NativeArray<Entity> unit;
        [ReadOnly] public NativeArray<Translation> unitPos;

        public void Execute(Entity entity, int index, ref Translation postition, ref Range range, ref Target target)
        {

            //only look for targets if unit doesn't already have one
            if (target.Entity == Entity.Null)
            {
                //make sure array is greater than 1
                if (unitPos.Length > 1)
                {
                    //loop through all entity positions
                    for (int i = 0; i < unitPos.Length; i++)
                    {
                        //make sure they are not checking themselves out
                        if (unit[i] != entity)
                        {
                            if (math.distance(unitPos[i].Value, postition.Value) <= range.MaxEngagement)
                            {
                                //set as entity that is in range as target. 
                                target.Entity = unit[i];
                            }
                        }
                    }
                }
            }
            else if(target.Entity != Entity.Null)
            {
                if(unitPos.Length > 1)
                {
                    for(int i = 0; i < unitPos.Length;i++)
                    {
                        if(unit[i] == target.Entity)
                        {
                            //if target is within min and max - attack
                            if (math.distance(unitPos[i].Value, postition.Value) >= range.Min
                                && math.distance(unitPos[i].Value, postition.Value) <= range.Max)
                            {
                                target.Action = UnitAction.Attack;
                            }
                            //if target is to close, move to min range..need to work on this one, currently if one runs the other will chase
                            //causing them both to endlessly run
                            else if(math.distance(unitPos[i].Value, postition.Value) <= range.Min)
                            {
                                //target.Action = UnitAction.Move;
                                //target.Destination = unitPos[i].Value + range.Min;
                            }
                            //if target is to far but within engagement zone move towards target
                            else if(math.distance(unitPos[i].Value, postition.Value) >= range.Max 
                                && math.distance(unitPos[i].Value, postition.Value) <= range.MaxEngagement)
                            {
                                target.Action = UnitAction.Move;
                                target.Destination = unitPos[i].Value;
                            }
                            //if target is out of engagement zone stop set target to null and stop moving
                            else if(math.distance(unitPos[i].Value, postition.Value) >= range.MaxEngagement)
                            {
                                target.Entity = Entity.Null;
                                target.Destination = postition.Value;
                                target.Action = UnitAction.Defend;
                            }
                        }
                    }
                }
            }
        }
    }
    


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //query of all entities with health
        EntityQuery m_units = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<Health>());

        //array of all entites from above query
        NativeArray<Entity> m_unitList = m_units.ToEntityArray(Allocator.TempJob);

        //array of all entites positions from above query
        NativeArray<Translation> m_unitPos = m_units.ToComponentDataArray<Translation>(Allocator.TempJob);

        var job = new UnitInRangeJob
        {
            unit = m_unitList,
            unitPos = m_unitPos
        };

        //schedule / complete
        var handle = job.Schedule(this, inputDeps);
        handle.Complete();

        //clean up
        m_unitPos.Dispose();
        m_unitList.Dispose();

        return handle;

    }
}
