using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class InitializePlayerTeamProxy : MonoBehaviour, IConvertGameObjectToEntity
{
	public int playerTeam = 1;

	public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{

		var team = new SingletonPlayerTeam
		{
			Team = playerTeam
		};
		dstManager.AddComponentData(entity, team);
	}
}