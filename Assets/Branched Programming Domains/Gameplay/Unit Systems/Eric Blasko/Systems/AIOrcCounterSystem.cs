using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using System;

public class AIOrcCounterSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var OrcAI = GetSingleton<OrcAIUnitSpawnerSingleton>();

        OrcAI.gameTime = Time.timeSinceLevelLoad;
        OrcAI.spawntime += Time.deltaTime;
        OrcAI.loadSpawner += Time.deltaTime;

        if (OrcAI.loadSpawner >= 60)
        {
            OrcAI.loadSpawner = 0;

            // if gameplay is under 5 minutes
            if (OrcAI.gameTime <= 300.0f)
            {
                for (int i = 0; i < OrcAI.multiplier; i++)
                {
                    var random = UnityEngine.Random.Range(0, 6);
                    switch (random)
                    {
                        case 0:
                            OrcAI.Brutes++;
                            break;
                        case 1:
                            OrcAI.Sluggers++;
                            break;
                        case 2:
                            OrcAI.Warlords++;
                            break;
                        case 3:
                            OrcAI.Raiders++;
                            break;
                        case 4:
                            OrcAI.Warlocks++;
                            break;
                        case 5:
                            OrcAI.Demolishers++;
                            break;
                        case 6:
                            OrcAI.Wyverns++;
                            break;

                    }
                }
            }
            //if gameplay is between 5-10 minutes
            else if (OrcAI.gameTime > 300.0f && OrcAI.gameTime < 600)
            {
                for (int i = 0; i < OrcAI.multiplier; i++)
                {
                    var random = UnityEngine.Random.Range(0, 6);
                    switch (random)
                    {
                        case 0:
                            OrcAI.Brutes++;
                            break;
                        case 1:
                            OrcAI.Sluggers++;
                            break;
                        case 2:
                            OrcAI.Warlords++;
                            break;
                        case 3:
                            OrcAI.Raiders++;
                            break;
                        case 4:
                            OrcAI.Warlocks++;
                            break;
                        case 5:
                            OrcAI.Demolishers++;
                            break;
                        case 6:
                            OrcAI.Wyverns++;
                            break;

                    }
                }
            }
            // if gameplay is between 10-15 minutes
            else if (OrcAI.gameTime > 600.0f && OrcAI.gameTime < 900)
            {
                for (int i = 0; i < OrcAI.multiplier; i++)
                {
                    var random = UnityEngine.Random.Range(0, 6);
                    switch (random)
                    {
                        case 0:
                            OrcAI.Brutes++;
                            break;
                        case 1:
                            OrcAI.Sluggers++;
                            break;
                        case 2:
                            OrcAI.Warlords++;
                            break;
                        case 3:
                            OrcAI.Raiders++;
                            break;
                        case 4:
                            OrcAI.Warlocks++;
                            break;
                        case 5:
                            OrcAI.Demolishers++;
                            break;
                        case 6:
                            OrcAI.Wyverns++;
                            break;

                    }
                }
            }
            //add more cases if needed
        }

        SetSingleton<OrcAIUnitSpawnerSingleton>(OrcAI);


        return inputDeps;
    }
}
