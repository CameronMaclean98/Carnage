using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class AddPlayerSingletonSpellKeys : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var keyboardSpells = new SingletonKeyboardSpells
        {
            QKey = false,
            QKeyDown = false,
            WKey = false,
            WKeyDown = false,
            EKey = false,
            EKeyDown = false,
            RKey = false,
            RKeyDown = false
        };
        dstManager.AddComponentData(entity, keyboardSpells);

        var SpellCoolDowns = new SingletonSpellCoolDown
        {
            QSpellTimer = 0,
            WSpellTimer = 0,
            ESpellTimer = 0,
            RSpellTimer = 0,

            QSpellMaxTimer = 30,
            WSpellMaxTimer = 20,
            ESpellMaxTimer = 10,
            RSpellMaxTimer = 2
        };
        dstManager.AddComponentData(entity, SpellCoolDowns);
    }
}
