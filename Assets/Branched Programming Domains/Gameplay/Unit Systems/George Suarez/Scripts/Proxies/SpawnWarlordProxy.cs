using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class SpawnWarlordProxy : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
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
        var tag = new TagWarlord
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

