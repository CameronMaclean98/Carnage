using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SacrificialProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
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
        var tag = new TagSacrificialPit
        {

        };
        var whatis = new TagBuilding
        {

        };
        dstManager.AddComponentData(entity, spawn);
        dstManager.AddComponentData(entity, tag);
        dstManager.AddComponentData(entity, whatis);

    }
}
