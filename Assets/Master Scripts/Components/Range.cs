using Unity.Entities;

public struct Range : IComponentData
{
                                    //edited by Eric Blasko
    public float MaxEngagement;     //Max value that unit will start engaging target
    public float Max;               //max attack range
    public float Min;               //min attack range
    public float Value;             //current range
}
