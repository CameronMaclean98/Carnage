using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

[UpdateAfter(typeof(KeyboardInputSystem))]
public class CameraMovementSystem : JobComponentSystem
{
    [BurstCompile]
    [RequireComponentTag(typeof(TagPlayer))]
    struct CameraMovementJob : IJobForEach<MovementSpeed, SingletonMouseInput, SingletonKeyboardInput, Translation>
    {
        [ReadOnly] public float deltaTime;
        [ReadOnly] public int screenHeight;
        [ReadOnly] public int screenWidth;
        [ReadOnly] public int panBorderThickness;

        public void Execute([ReadOnly] ref MovementSpeed movementSpeed, [ReadOnly] ref SingletonMouseInput mouseInput, [ReadOnly] ref SingletonKeyboardInput keyboardInput, ref Translation position)
        {
            // Move Camera towards forward or backward direction
            if ( mouseInput.CurrentMousePosition.y >= screenHeight - panBorderThickness )
            {
                position.Value.z += MouseScrollSensitivity(mouseInput.CurrentMousePosition.y, screenHeight - panBorderThickness) * movementSpeed.Value * deltaTime;
            }
            else if ( mouseInput.CurrentMousePosition.y <= panBorderThickness )
            {
                position.Value.z -= MouseScrollSensitivity(mouseInput.CurrentMousePosition.y, panBorderThickness) * movementSpeed.Value * deltaTime;
            }
            // Move Camera towards left or right direction
            if ( mouseInput.CurrentMousePosition.x <= panBorderThickness )
            {
                position.Value.x -= MouseScrollSensitivity(mouseInput.CurrentMousePosition.x, panBorderThickness) * movementSpeed.Value * deltaTime;
            }
            else if ( mouseInput.CurrentMousePosition.x >= screenWidth - panBorderThickness )
            {
                position.Value.x += MouseScrollSensitivity(mouseInput.CurrentMousePosition.x, screenWidth - panBorderThickness) * movementSpeed.Value * deltaTime;
            }
            // Move Camera based on directional keypad(s) being pressed
            if ( keyboardInput.HorizontalMovement != 0 || keyboardInput.VerticalMovement != 0 )
            {
                float moveSpeed = movementSpeed.Value * deltaTime;

                position.Value.x += keyboardInput.HorizontalMovement * moveSpeed;
                position.Value.z += keyboardInput.VerticalMovement * moveSpeed;
            }
        }

        public float MouseScrollSensitivity(float mouseAxis, float screenBorder)
        {
            float sensitivity = math.abs(mouseAxis - screenBorder) / 100;

            return sensitivity;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var cameraMovementJob = new CameraMovementJob
        {
            deltaTime = Time.deltaTime,
            screenHeight = Screen.height,
            screenWidth = Screen.width,
            panBorderThickness = 5,
        };

        return cameraMovementJob.Schedule(this, inputDeps);
    }
}
