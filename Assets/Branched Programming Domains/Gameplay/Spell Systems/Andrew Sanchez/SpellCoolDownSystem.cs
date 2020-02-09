//system to move the damage entity, entity follows current mouse raycast position

using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;

public class SpellCoolDownSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var spellCooldown = GetSingleton<SingletonSpellCoolDown>();

        if (spellCooldown.QSpellTimer > 0)
        {
            spellCooldown.QSpellTimer -= Time.deltaTime;
        }
        else spellCooldown.QSpellTimer = 0;

        if (spellCooldown.WSpellTimer > 0)
        {
            spellCooldown.WSpellTimer -= Time.deltaTime;
        }
        else spellCooldown.WSpellTimer = 0;

        if (spellCooldown.ESpellTimer > 0)
        {
            spellCooldown.ESpellTimer -= Time.deltaTime;
        }
        else spellCooldown.ESpellTimer = 0;

        if (spellCooldown.RSpellTimer > 0)
        {
            spellCooldown.RSpellTimer -= Time.deltaTime;
        }
        else spellCooldown.RSpellTimer = 0;

        SetSingleton<SingletonSpellCoolDown>(spellCooldown);

        return inputDeps;
    }
}
