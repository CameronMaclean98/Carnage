using Unity.Entities;

public struct SingletonKeyboardSpells : IComponentData
{
    public bool QKey;
    public bool QKeyDown;
    public bool WKey;
    public bool WKeyDown;
    public bool EKey;
    public bool EKeyDown;
    public bool RKey;
    public bool RKeyDown;
}
