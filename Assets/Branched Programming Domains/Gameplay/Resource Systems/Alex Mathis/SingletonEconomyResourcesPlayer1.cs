using Unity.Entities;

public struct SingletonEconomyResourcesPlayer1 : IComponentData
{
    public float Gold; // The amount of gold the player has in resources
    public float Lumber; // The amount of Lumber the player has in resources
    public float Favor; // The amount of Favor the player has in resources
    public float Population; // The amount of Population the player has in total from their army
    public float MaxPopulation; // The maximum amount of Population that cannot be exceeded
	
	//Added by Alex Mathis
	public float ResourceAddTimer; //When this value is 0, add resources
	public float OriginalResourceAddTimer; //ResourceAddTimer is reset to this value when it reaches 0
}
