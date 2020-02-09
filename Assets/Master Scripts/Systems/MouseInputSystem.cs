using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class MouseInputSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mouseInput = GetSingleton<SingletonMouseInput>();

        mouseInput.CurrentMousePosition = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(mouseInput.CurrentMousePosition);
       
        UnityEngine.RaycastHit hit;
        if ( Physics.Raycast(ray, out hit) )
        {
            mouseInput.CurrentMouseRaycastPosition = hit.point;
        }

        if ( Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) )
        {
            mouseInput.InitialMouseClickPosition = Input.mousePosition;
            mouseInput.InitialMouseRaycastPosition = mouseInput.CurrentMouseRaycastPosition;
        }

        if ( Input.GetMouseButton(0) )
        {
            mouseInput.LeftClickDown = true;
        }
        else if ( Input.GetMouseButtonUp(0) )
        {
            mouseInput.LeftClickUp = true;
        }
        else
        {
            mouseInput.LeftClickUp = false;
            mouseInput.LeftClickDown = false;
        }
        if ( Input.GetMouseButton(1) )
        {
            mouseInput.RightClickDown = true;
        }
        else if ( Input.GetMouseButtonUp(1) )
        {
            mouseInput.RightClickUp = true;
        }
        else
        {
            mouseInput.RightClickUp = false;
            mouseInput.RightClickDown = false;
        }

        SetSingleton<SingletonMouseInput>(mouseInput);

        return inputDeps;
    }
}

/*
 * var direction = ray.direction - mouseInput.MousePosition;
 * 
       RaycastInput raycastInput = new RaycastInput()
       {
           Ray = new Unity.Physics.Ray()
           {
               Origin = mousePos,
               Direction = direction
           },
           Filter = new CollisionFilter()
           {
               CategoryBits = ~0u,
               MaskBits = ~0u,
               GroupIndex = 0
           }
       };
       Unity.Physics.RaycastHit hit;

       var physicsWorldSystem = Unity.Entities.World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
       var collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;

       bool hasHit = collisionWorld.CastRay(raycastInput, out hit);
       if ( hasHit )
       {
           mouseInput.MouseRaycastPosition = hit.Position;
       }
*/