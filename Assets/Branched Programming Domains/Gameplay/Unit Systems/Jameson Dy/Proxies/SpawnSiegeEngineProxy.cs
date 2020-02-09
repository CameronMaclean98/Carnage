using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SpawnSiegeEngineProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject prefab;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(prefab);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spawn = new SpawnUnit
        {
            Prefab = conversionSystem.GetPrimaryEntity(prefab)
        };
        var tag = new TagSiegeEngine
        {

        };
        var whatis = new TagUnit
        {

        };
        dstManager.AddComponentData(entity, spawn);
        dstManager.AddComponentData(entity, tag);
        dstManager.AddComponentData(entity, whatis);

    }
}
