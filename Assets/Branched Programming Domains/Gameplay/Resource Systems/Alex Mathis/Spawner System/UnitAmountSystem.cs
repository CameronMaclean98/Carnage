using Unity.Jobs;
using Unity.Entities;
using UnityEngine;

public class UnitAmountSystem : ComponentSystem {

	protected override void OnUpdate() {

		int LegionnaireGold = 75;
		int MarksmenGold = 50;
		int PaladinGold = 300;
		int KnightGold = 150;
		int SiegeEngineGold = 600;
		int SpellSlingerGold = 100;
		int GriffinGold = 400;


			var UnitSpawning = GetSingleton<UnitSpawnerSingleton>();
			var keyboardInput = GetSingleton<SingletonKeyboardInput>();
			var resources = GetSingleton<SingletonEconomyResources>();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                keyboardInput.one_key = true;
				if (resources.Gold >= LegionnaireGold) {
					resources.Gold -= LegionnaireGold;
					SetSingleton<SingletonEconomyResources>(resources);
					UnitSpawning.Legionnaires++;
				}
            }
            else
            {
                keyboardInput.one_key = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                keyboardInput.two_key = true;
				if (resources.Gold >= MarksmenGold) {
					resources.Gold -= MarksmenGold;
					SetSingleton<SingletonEconomyResources>(resources);
					UnitSpawning.Marksmen++;
				}
            }
            else
            {
                keyboardInput.two_key = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                keyboardInput.three_key = true;
				if (resources.Gold >= PaladinGold) {
					resources.Gold -= PaladinGold;
					SetSingleton<SingletonEconomyResources>(resources);
					UnitSpawning.Paladins++;
				}
            }
            else
            {
                keyboardInput.three_key = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                keyboardInput.four_key = true;
				if (resources.Gold >= KnightGold) {
					resources.Gold -= KnightGold;
					SetSingleton<SingletonEconomyResources>(resources);
					UnitSpawning.Knights++;
				}
            }
            else
            {
                keyboardInput.four_key = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                keyboardInput.five_key = true;
				if (resources.Gold >= SiegeEngineGold) {
					resources.Gold -= SiegeEngineGold;
					SetSingleton<SingletonEconomyResources>(resources);
					UnitSpawning.SiegeEngines++;
				}
            }
            else
            {
                keyboardInput.five_key = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                keyboardInput.six_key = true;
				if (resources.Gold >= SpellSlingerGold) {
					resources.Gold -= SpellSlingerGold;
					SetSingleton<SingletonEconomyResources>(resources);
					UnitSpawning.SpellSlingers++;
				}
            }
            else
            {
                keyboardInput.six_key = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                keyboardInput.seven_key = true;
				if (resources.Gold >= GriffinGold) {
					resources.Gold -= GriffinGold;
					SetSingleton<SingletonEconomyResources>(resources);
					UnitSpawning.Griffins++;
				}
            }
            else
            {
                keyboardInput.seven_key = false;
            }

            SetSingleton<SingletonKeyboardInput>(keyboardInput);
            SetSingleton<UnitSpawnerSingleton>(UnitSpawning);
			SetSingleton<SingletonEconomyResources>(resources);

	}

}


