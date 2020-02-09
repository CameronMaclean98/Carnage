using Unity.Entities;
using UnityEngine;

//Cameron edited the name of the system so that we can run tests for other systems
public class ResourceGainSystem : ComponentSystem
{
	
    protected override void OnUpdate()
    {
        var playerResources = GetSingleton<SingletonEconomyResources>();
		var playerTeam = GetSingleton<SingletonPlayerTeam>();
		
        //Increase Values
		if (playerResources.ResourceAddTimer > 0) {
					playerResources.ResourceAddTimer -= Time.deltaTime;
			}
			else {

		        //Add Resources
		        //Add passive resource gain
		        int passiveGold = 5;
		        int passiveLumber = 5;
		        int passiveFavor = 5;
		        int goldPerBuilding = 10;
		        int lumberPerBuilding = 10;
		        int favorPerAltar = 10;
				int team = playerTeam.Team;

		        playerResources.Gold += passiveGold;
		        playerResources.Lumber += passiveLumber;
		        playerResources.Favor += passiveFavor;
					
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
			playerResources.Gold += (ownedGoldBuildings * goldPerBuilding);
				    playerResources.Lumber += (ownedLumberBuildings * lumberPerBuilding);
				    playerResources.Favor += (ownedAltars * favorPerAltar);



		        //Reset Timer
		        playerResources.ResourceAddTimer = playerResources.OriginalResourceAddTimer;
			}
			
		SetSingleton<SingletonEconomyResources>(playerResources);

    }
	
}
