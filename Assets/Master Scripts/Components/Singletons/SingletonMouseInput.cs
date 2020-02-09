using Unity.Entities;
using Unity.Mathematics;

public struct SingletonMouseInput : IComponentData
{
    public bool LeftClickDown; // Left Click is pressed and registered once
    public bool RightClickDown; // Right Click is pressed and registered once
    public bool LeftClick; // Left Click is pressed and is continuously registered
    public bool RightClick; // Right Click is pressed and is continuously registered
    public bool LeftClickUp; // Left Click is released and registered once
    public bool RightClickUp; // Right Click is released and registered once
    public float3 InitialMouseClickPosition; // When clicked, the screen mouse position is registered once
    public float3 CurrentMousePosition;  // The screen mouse position is continuously registered
    public float3 InitialMouseRaycastPosition; // When clicked, the raycast mouse position is registered once
    public float3 CurrentMouseRaycastPosition; // The raycast mouse position is continuously registered
}