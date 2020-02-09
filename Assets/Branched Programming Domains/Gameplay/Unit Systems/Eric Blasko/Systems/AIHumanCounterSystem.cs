using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using System;

public class AIHumanCounterSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var HumanAI = GetSingleton<HumanAIUnitSpawnerSingleton>();

        HumanAI.gameTime = Time.timeSinceLevelLoad;
        HumanAI.spawntime += Time.deltaTime;
        HumanAI.loadSpawner += Time.deltaTime;

        if (HumanAI.loadSpawner >= 60)
        {
            HumanAI.loadSpawner = 0;

            // if gameplay is under 5 minutes
            if (HumanAI.gameTime <= 300.0f)
            {
                for (int i = 0; i < HumanAI.multiplier; i++)
                {
                    var random = UnityEngine.Random.Range(0, 6);
                    switch (random)
                    {
                        case 0:
                            //loads counter
                            HumanAI.Knights++;
                            break;
                        case 1:
                            HumanAI.Paladins++;
                            break;
                        case 2:
                            HumanAI.SpellSlingers++;
                            break;
                        case 3:
                            HumanAI.Legionnaires++;
                            break;
                        case 4:
                            HumanAI.Marksmen++;
                            break;
                        case 5:
                            HumanAI.SiegeEngines++;
                            break;
                        case 6:
                            HumanAI.Griffins++;
                            break;

                    }
                }
            }
            //if gameplay is between 5-10 minutes
            else if (HumanAI.gameTime > 300.0f && HumanAI.gameTime < 600)
            {
                for (int i = 0; i < HumanAI.multiplier; i++)
                {
                    var random = UnityEngine.Random.Range(0, 6);
                    switch (random)
                    {
                        case 0:
                            //loads counter
                            HumanAI.Knights++;
                            break;
                        case 1:
                            HumanAI.Paladins++;
                            break;
                        case 2:
                            HumanAI.SpellSlingers++;
                            break;
                        case 3:
                            HumanAI.Legionnaires++;
                            break;
                        case 4:
                            HumanAI.Marksmen++;
                            break;
                        case 5:
                            HumanAI.SiegeEngines++;
                            break;
                        case 6:
                            HumanAI.Griffins++;
                            break;

                    }
                }
            }
            // if gameplay is between 10-15 minutes
            else if (HumanAI.gameTime > 600.0f && HumanAI.gameTime < 900)
            {
                for (int i = 0; i < HumanAI.multiplier; i++)
                {
                    var random = UnityEngine.Random.Range(0, 6);
                    switch (random)
                    {
                        case 0:
                            //loads counter
                            HumanAI.Knights++;
                            break;
                        case 1:
                            HumanAI.Paladins++;
                            break;
                        case 2:
                            HumanAI.SpellSlingers++;
                            break;
                        case 3:
                            HumanAI.Legionnaires++;
                            break;
                        case 4:
                            HumanAI.Marksmen++;
                            break;
                        case 5:
                            HumanAI.SiegeEngines++;
                            break;
                        case 6:
                            HumanAI.Griffins++;
                            break;

                    }
                }
            }
            //add more cases if needed
        }
  
        SetSingleton<HumanAIUnitSpawnerSingleton>(HumanAI);


        return inputDeps;
    }
}