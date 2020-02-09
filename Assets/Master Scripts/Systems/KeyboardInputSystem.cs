using Unity.Jobs;
using Unity.Entities;
using UnityEngine;

public class KeyboardInputSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var keyboardInput = GetSingleton<SingletonKeyboardInput>();

        keyboardInput.HorizontalMovement = Input.GetAxis("Horizontal");
        keyboardInput.VerticalMovement = Input.GetAxis("Vertical");


        // David Cruz Edit START: I added these lines of code so that whatever the user selected last is the spell that is saved
        if (Input.GetKeyDown(KeyCode.Q))
        {
            keyboardInput.selectedSpell = KeyCode.Q;
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            keyboardInput.selectedSpell = KeyCode.W;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            keyboardInput.selectedSpell = KeyCode.E;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            keyboardInput.selectedSpell = KeyCode.R;
        }
        


            keyboardInput.HorizontalMovement = Input.GetAxis("Horizontal");
            keyboardInput.VerticalMovement = Input.GetAxis("Vertical");

            

            SetSingleton<SingletonKeyboardInput>(keyboardInput);
           

            return inputDeps;
        }
    }

        // David Cruz Edit END:


        // added function in order to spawsn enemy units by pressing Z
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    keyboardInput.selectedSpell = KeyCode.Z;
        //}
        //
