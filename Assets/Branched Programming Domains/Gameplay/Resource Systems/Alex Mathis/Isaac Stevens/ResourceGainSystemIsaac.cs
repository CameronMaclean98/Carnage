using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using Unity.Transforms;

//Cameron edited the name of the system so that we can run tests for other systems
public class ResourceGainSystemIsaac : ComponentSystem
{
	
	
	
    protected override void OnUpdate()
    {

		//Disables this script
		this.Enabled = false;

        var playerResources = GetSingleton<SingletonEconomyResources>();

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

            playerResources.Gold += passiveGold;
            playerResources.Lumber += passiveLumber;
            playerResources.Favor += passiveFavor;

            
            
				
				//Count number of gold producing buildings belonging to player 1
				EntityQuery entities = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<FavorProducerComponent>(), ComponentType.ReadOnly<Player1Tag>());
				int ownedAltars = entities.CalculateLength();

                EntityQuery entities2 = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<LumberProducerComponent>(), ComponentType.ReadOnly<Player1Tag>());
                int ownedLumberBuildings = entities2.CalculateLength();

                EntityQuery entities3 = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<GoldProducerComponent>(), ComponentType.ReadOnly<Player1Tag>());
                int ownedGoldBuildings = entities3.CalculateLength();

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