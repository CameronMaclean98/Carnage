using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

public class ColliderSystem : MonoBehaviour
{
    public unsafe Entity SphereCast(float3 RayFrom, float3 RayTo, float radius)
    {
        var physicsWorldSystem = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
        var collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;

        var filter = new CollisionFilter()
        {
            BelongsTo = ~0u, // all 1s, so all layers, collide with everything 
            CollidesWith = ~0u,
            GroupIndex = 0
        };

        BlobAssetReference<Unity.Physics.Collider> sphereCollider = Unity.Physics.SphereCollider.Create(float3.zero, radius, filter);

        ColliderCastInput input = new ColliderCastInput()
        {
            Start = RayFrom,
            Orientation = quaternion.identity,
            Direction = RayTo - RayFrom,
            Collider = (Unity.Physics.Collider*)sphereCollider.GetUnsafePtr()
        };

        ColliderCastHit hit = new ColliderCastHit();
        bool haveHit = collisionWorld.CastCollider(input, out hit);
        if (haveHit)
        {
            // see hit.Position 
            // see hit.SurfaceNormal
            Entity e = physicsWorldSystem.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
            Debug.Log("I Collided!");
            return e;
        }
        return Entity.Null;
    }


    [BurstCompile]
    public struct ColliderCastJob : IJobParallelFor
    {
        [ReadOnly] public Unity.Physics.CollisionWorld world;
        [ReadOnly] public NativeArray<Unity.Physics.ColliderCastInput> inputs;
        public NativeArray<Unity.Physics.ColliderCastHit> results;

        public unsafe void Execute(int index)
        {
            Unity.Physics.ColliderCastHit hit;
            world.CastCollider(inputs[index], out hit);
            results[index] = hit;
        }
    }
    public static JobHandle ScheduleBatchRayCast(Unity.Physics.CollisionWorld world,
    NativeArray<Unity.Physics.ColliderCastInput> inputs, NativeArray<Unity.Physics.ColliderCastHit> results)
    {
        JobHandle rcj = new ColliderCastJob
        {
            inputs = inputs,
            results = results,
            world = world

        }.Schedule(inputs.Length, 5);
        return rcj;
    }
}