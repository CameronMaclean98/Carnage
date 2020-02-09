using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;

public class RateSystem : JobComponentSystem
{
    struct RateJob : IJobForEachWithEntity<Rate>
    {
        public float deltaTime;

        public void Execute(Entity entity, int index, ref Rate rate)
        {
            if (rate.Value < rate.Max)
            {
                rate.Value += deltaTime;
                if (rate.Value > rate.Max)
                    rate.Cooldown = 1;
            }

        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new RateJob
        {
            deltaTime = Time.deltaTime
        };

        var handle = job.Schedule(this, inputDeps);
        return handle;
    }
}