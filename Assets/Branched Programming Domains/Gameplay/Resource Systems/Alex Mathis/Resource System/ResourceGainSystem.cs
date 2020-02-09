using Unity.Entities;
using UnityEngine;

//Cameron edited the name of the system so that we can run tests for other systems
public class ResourceGainSystem : ComponentSystem
{
	float gameTime = 0;
	int passiveGold = 50;
	int passiveLumber = 50;
	int passiveFavor = 50;
	int goldPerBuilding = 100;
	int lumberPerBuilding = 100;
	int favorPerAltar = 100;
	int resourceMultiplier = 1;

	protected override void OnUpdate()
    {
        var playerResources = GetSingleton<SingletonEconomyResources>();
		var playerTeam = GetSingleton<SingletonPlayerTeam>();


		gameTime += Time.deltaTime;
		//Every 10 minutes increase resource gain (1x->2x->3x->...)
		if (gameTime > 600)
		{
			gameTime = 0;
			resourceMultiplier++;		
		}


        //Increase Values
		if (playerResources.ResourceAddTimer > 0) {
					playerResources.ResourceAddTimer -= Time.deltaTime;
			}
			else {

		        //Add Resources
		        //Add passive resource gain
		        
				int team = playerTeam.Team;

		        playerResources.Gold += passiveGold * resourceMultiplier;
		        playerResources.Lumber += passiveLumber * resourceMultiplier;
		        playerResources.Favor += passiveFavor * resourceMultiplier;
					
				//Count number of gold producing buildings belonging to player 1
				int ownedAltars = 0;

				Entities.ForEach((Entity e, ref FavorProducerComponent favor, ref Team entityTeam) => {
					if (entityTeam.Number == team)
					{
						ownedAltars += 1;
					}
				});
					/*
					Entities.ForEach((Entity e) => {
							if (EntityManager.HasComponent(e, typeof(TagAltar))) {
								ownedAltars += 1;
								Debug.Log("Counting Altar \n");
							}


					});
					*/

			int ownedLumberBuildings = 0;
				Entities.ForEach((Entity e, ref LumberProducerComponent favor, ref Team entityTeam) => {
					if (entityTeam.Number == team)
					{
						ownedLumberBuildings += 1;
					}
				});

			int ownedGoldBuildings = 0;
				Entities.ForEach((Entity e, ref GoldProducerComponent favor, ref Team entityTeam) => {
					if (entityTeam.Number == team)
					{
						ownedGoldBuildings += 1;
					}
				});

			//Add the gold from the buildings 
			playerResources.Gold += (ownedGoldBuildings * goldPerBuilding * resourceMultiplier);
				    playerResources.Lumber += (ownedLumberBuildings * lumberPerBuilding * resourceMultiplier);
				    playerResources.Favor += (ownedAltars * favorPerAltar * resourceMultiplier);



		        //Reset Timer
		        playerResources.ResourceAddTimer = playerResources.OriginalResourceAddTimer;
			}
			
		SetSingleton<SingletonEconomyResources>(playerResources);

    }
	
}
