using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class AddResourceEconomyProxy : MonoBehaviour, IConvertGameObjectToEntity
{
	public int startingGold = 100;
	public int startingLumber = 100;
	public int startingFavor = 100;
	public int startingPopulation = 1;
	public int maxPopulation = 999;
	public int resourceAddTimer = 3;

	public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{

		var resources = new SingletonEconomyResources
		{
			Gold = startingGold,
			Lumber = startingLumber,
			Favor = startingFavor,
			Population = startingPopulation,
			MaxPopulation = maxPopulation,
			ResourceAddTimer = resourceAddTimer,
			OriginalResourceAddTimer = resourceAddTimer
		};
		dstManager.AddComponentData(entity, resources);
	}
}