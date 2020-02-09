using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Teams
{
    TEAM1,
    TEAM2,
    TEAM3,
    TEAM4,
    TEAM5,
    TEAM6
}
/// <summary>
/// This determines the god image
/// </summary>
public enum GodSprite
{
    NONE,
    CebilIMAGE,
    FermundIMAGE,
    BrannoIMAGE,
    MalbainIMAGE,
    TherosIMAGE,
    NimaIMAGE
}

/// <summary>
/// Choose a God for the team
/// </summary>
public enum God
{
    Cebil,
    Fermund,
    Branno,
    Malbain,
    Theros,
    Nima,
    Random
};

public enum PlayerColor
{
    RED,
    BLUE,
    GREEN,
    YELLOW,
    PURPLE,
    ORANGE
}
public enum Status
{
    Open,
    Closed,
    AIMode
};
/// <summary>
/// This data should be used in a GameManager script.
/// </summary>
[CreateAssetMenu]

public class TeamData : ScriptableObject
{
    public Status PAI; //Records whether a slot is a player, closed, or AI 
    public Teams teamNumber;
    public God god;
    public PlayerColor playerColor;
}


