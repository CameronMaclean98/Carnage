using Unity.Entities;

public struct GoldProducerComponent : IComponentData
{
	public int amountProduced; //Amount produced each time timer == 0
}
