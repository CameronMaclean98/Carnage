using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CebilSpellsProxy : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject AttackBanner;
    public GameObject DefenseBanner;
    

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spells = new CebilSpells
        {
            AttackBanner = conversionSystem.GetPrimaryEntity(AttackBanner),
            DefenseBanner = conversionSystem.GetPrimaryEntity(DefenseBanner)
        };
        dstManager.AddComponentData(entity, spells);

        var god = new TagCebil
        {

        };
        dstManager.AddComponentData(entity, god);
    }

    public void DeclareReferencedPrefabs(List<GameObject> gameObjects)
    {
        gameObjects.Add(AttackBanner);
        gameObjects.Add(DefenseBanner);
    }

    
    
}
