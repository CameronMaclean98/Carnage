using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class SpawnLightningProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject prefab;
    //public GameObject testMeteor;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(prefab);
        //referencedPrefabs.Add(testMeteor);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spawn = new MeteorSpawner
        {
            Prefab = conversionSystem.GetPrimaryEntity(prefab),
            //TestMeteor = conversionSystem.GetPrimaryEntity(prefab)
        };
        dstManager.AddComponentData(entity, spawn);
    }
}
