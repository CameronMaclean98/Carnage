using Unity.Jobs;
using Unity.Entities;
using UnityEngine;

public class KeyboardSpellInputSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var keyboardSpellInput = GetSingleton<SingletonKeyboardSpells>();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            keyboardSpellInput.QKeyDown = true;
        }
        else
            keyboardSpellInput.QKeyDown = false;

        if (Input.GetKeyDown(KeyCode.W))
        {
            keyboardSpellInput.WKeyDown = true;
        }
        else
            keyboardSpellInput.WKeyDown = false;

        if (Input.GetKeyDown(KeyCode.E))
        {
            keyboardSpellInput.EKeyDown = true;
        }
        else
            keyboardSpellInput.EKeyDown = false;

        if (Input.GetKeyDown(KeyCode.R))
        {
            keyboardSpellInput.RKeyDown = true;
        }
        else
            keyboardSpellInput.RKeyDown = false;

        SetSingleton<SingletonKeyboardSpells>(keyboardSpellInput);

        return inputDeps;
    }
}