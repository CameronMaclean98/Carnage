using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;

public class ResourceCaptureSystem : JobComponentSystem
{

	
	

	[RequireComponentTag(typeof(ResourceTag))]
	struct ResourceCaptureJob : IJobForEachWithEntity<Translation, Team, ResourceTag>
	{
		public NativeArray<Translation> unitPositions;
		public NativeArray<Team> unitTeams;
		public float deltaTime;
		public int playerTeam;

		public void Execute(Entity e, int i, [ReadOnly] ref Translation translation, ref Team team, ref ResourceTag tag)
		{
			int nearbyUnits = 0;
			bool contested = false;
			int capturingTeam = 0;
			int captureRadius = 10;
			int captureTimerThreshold = 5;

			for (int j = 0; i < unitPositions.Length; i++)
			{
				float distance = math.distance(translation.Value, unitPositions[i].Value);

				if (distance < captureRadius) {
			
					if (team.Number != unitTeams[i].Number)
					{
						capturingTeam = unitTeams[i].Number;
						nearbyUnits++;
					}
					else {
						contested = true;
					}
				}
			}

			if (!contested) {
				if (tag.captureTimer > captureTimerThreshold) {
					tag.captureTimer = 0;
					team.Number = capturingTeam;
				}
				if (nearbyUnits > 1) {
					tag.captureTimer += deltaTime;
				}
			}			


		}

	};

	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{

		var jobHandle = inputDeps;

		EntityQuery entities = GetEntityQuery(ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<TagUnit>(), ComponentType.ReadOnly<Team>());

		NativeArray<Translation> translations = entities.ToComponentDataArray<Translation>(Allocator.TempJob);
		NativeArray<Team> teams = entities.ToComponentDataArray<Team>(Allocator.TempJob);

		var playerTeam = GetSingleton<SingletonPlayerTeam>();

		var resourceJob = new ResourceCaptureJob
		{
			unitPositions = translations,
			unitTeams = teams,
			deltaTime = Time.deltaTime,
			playerTeam = playerTeam.Team
		};

		var resourceJobHandle = resourceJob.ScheduleSingle(this, jobHandle);

		resourceJobHandle.Complete();
		
		translations.Dispose();
		teams.Dispose();

		return jobHandle;
	}
}
