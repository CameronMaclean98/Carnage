using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class FermundSpellsProxy : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject HealingWard;
    public GameObject Aura;
   

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spells = new FermundSpells
        {
            HealWard = conversionSystem.GetPrimaryEntity(HealingWard),
            Aura = conversionSystem.GetPrimaryEntity(Aura)
           
        };
        dstManager.AddComponentData(entity, spells);

    
    }

    public void DeclareReferencedPrefabs(List<GameObject> gameObjects)
    {
        gameObjects.Add(HealingWard);
        gameObjects.Add(Aura);
    }



}
