using Unity.Entities;

public struct LumberProducerComponent : IComponentData
{
	public int amountProduced; //Amount produced each time timer == 0
}
