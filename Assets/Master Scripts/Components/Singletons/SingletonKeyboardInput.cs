using Unity.Entities;
using UnityEngine;

/*TODO:
 * Read basic Hotkeys for RTS games: https://starcraft.fandom.com/wiki/Hotkey
 * 
 * Should split hotkeys across multiple Singletons
 * 
 */
public struct SingletonKeyboardInput : IComponentData
{
    // David Cruz EDIT: added selectedSpell so that we could store the last spell the user selected.
    public KeyCode selectedSpell; // this will store the last selected spell.
    //

    public bool SpaceBar;
    public float HorizontalMovement;
    public float VerticalMovement;
    public bool one_key;
    public bool two_key;
    public bool three_key;
    public bool four_key;
    public bool five_key;
    public bool six_key;
    public bool seven_key;

}
