using Unity.Entities;

public struct FavorProducerComponent : IComponentData
{
	public int amountProduced; //Amount produced each time timer == 0
}
