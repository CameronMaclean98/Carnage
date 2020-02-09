using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMPro.TMP_Dropdown GodP1, GodP2, GodP3, GodP4, GodP5, GodP6,
        Team1, Team2, Team3, Team4, Team5, Team6, 
        ColorP1, ColorP2, ColorP3, ColorP4, ColorP5, ColorP6, 
        StatP2, StatP3, StatP4, StatP5, StatP6;
    public TMPro.TMP_Text NameP1, NameP2, NameP3, NameP4, NameP5, NameP6;



    public TeamData Player1, Player2, Player3, Player4, Player5, Player6; // Declare teams and drag team asset objects into inspector
    public God[] GodPlayers = new God[5];
    public Teams[] Team = new Teams[5];
    public Status[] Stat = new Status[2];
    public PlayerColor[] Color = new PlayerColor[5];


     void Start()
     {

     }
    


     void Update()
     {
        GodPlayers = SwitchGod(GodP1, GodP2, GodP3, GodP4, GodP5, GodP6);
        Team = SwitchTeam(Team1, Team2, Team3, Team4, Team5, Team6);
        Stat = SwitchStatus(StatP2, StatP3, StatP4, StatP5, StatP6);
        Color = SwitchColor(ColorP1, ColorP2, ColorP3, ColorP4, ColorP5, ColorP6);


        if (Player1)
        {
            SavePlayer1Data(Stat[0], Team[0], GodPlayers[0], Color[0]);
        }
        if (Player2)
        {
            SavePlayer2Data(Stat[0], Team[0], GodPlayers[0], Color[0]);
        }
        if (Player3)
        {
            SavePlayer3Data(Stat[0], Team[0], GodPlayers[0], Color[0]);
        }
        if (Player4)
        {
            SavePlayer4Data(Stat[0], Team[0], GodPlayers[0], Color[0]);
        }
        if (Player5)
        {
            SavePlayer5Data(Stat[0], Team[0], GodPlayers[0], Color[0]);
        }
        if (Player6)
        {
            SavePlayer6Data(Stat[0], Team[0], GodPlayers[0], Color[0]);
        }

    }

    public God[] SwitchGod(TMPro.TMP_Dropdown p1, TMPro.TMP_Dropdown p2, TMPro.TMP_Dropdown p3, TMPro.TMP_Dropdown p4, TMPro.TMP_Dropdown p5, TMPro.TMP_Dropdown p6)
    {
        God[] list = new God[5];
        switch (p1.value)
        {
            case 0:
                list[0] = God.Cebil;
                break;
            case 1:
                list[1] = God.Fermund;
                break;
            case 2:
                list[2] = God.Branno;
                break;
            case 3:
                list[3] = God.Malbain;
                break;
            case 4:
                list[4] = God.Theros;
                break;
            case 5:
                list[5] = God.Nima;
                break;
        }

        switch (p2.value)
        {
            case 0:
                list[0] = God.Cebil;
                break;
            case 1:
                list[1] = God.Fermund;
                break;
            case 2:
                list[2] = God.Branno;
                break;
            case 3:
                list[3] = God.Malbain;
                break;
            case 4:
                list[4] = God.Theros;
                break;
            case 5:
                list[5] = God.Nima;
                break;
        }
        switch (p3.value)
        {
            case 0:
                list[0] = God.Cebil;
                break;
            case 1:
                list[1] = God.Fermund;
                break;
            case 2:
                list[2] = God.Branno;
                break;
            case 3:
                list[3] = God.Malbain;
                break;
            case 4:
                list[4] = God.Theros;
                break;
            case 5:
                list[5] = God.Nima;
                break;
        }
        switch (p4.value)
        {
            case 0:
                list[0] = God.Cebil;
                break;
            case 1:
                list[1] = God.Fermund;
                break;
            case 2:
                list[2] = God.Branno;
                break;
            case 3:
                list[3] = God.Malbain;
                break;
            case 4:
                list[4] = God.Theros;
                break;
            case 5:
                list[5] = God.Nima;
                break;
        }
        switch (p5.value)
        {
            case 0:
                list[0] = God.Cebil;
                break;
            case 1:
                list[1] = God.Fermund;
                break;
            case 2:
                list[2] = God.Branno;
                break;
            case 3:
                list[3] = God.Malbain;
                break;
            case 4:
                list[4] = God.Theros;
                break;
            case 5:
                list[5] = God.Nima;
                break;
        }
        switch (p6.value)
        {
            case 0:
                list[0] = God.Cebil;
                break;
            case 1:
                list[1] = God.Fermund;
                break;
            case 2:
                list[2] = God.Branno;
                break;
            case 3:
                list[3] = God.Malbain;
                break;
            case 4:
                list[4] = God.Theros;
                break;
            case 5:
                list[5] = God.Nima;
                break;
        }

        return list;
    }
    public Teams[] SwitchTeam(TMPro.TMP_Dropdown t1, TMPro.TMP_Dropdown t2, TMPro.TMP_Dropdown t3, TMPro.TMP_Dropdown t4, TMPro.TMP_Dropdown t5, TMPro.TMP_Dropdown t6)
    {
        Teams[] list = new Teams[5];

        switch (t1.value)
        {
            case 0:
                list[0] = Teams.TEAM1;
                break;
            case 1:
                list[1] = Teams.TEAM2;
                break;
            case 2:
                list[2] = Teams.TEAM3;
                break;
            case 3:
                list[3] = Teams.TEAM4;
                break;
            case 4:
                list[4] = Teams.TEAM5;
                break;
            case 5:
                list[5] = Teams.TEAM6;
                break;
        }

        switch (t2.value)
        {
            case 0:
                list[0] = Teams.TEAM1;
                break;
            case 1:
                list[1] = Teams.TEAM2;
                break;
            case 2:
                list[2] = Teams.TEAM3;
                break;
            case 3:
                list[3] = Teams.TEAM4;
                break;
            case 4:
                list[4] = Teams.TEAM5;
                break;
            case 5:
                list[5] = Teams.TEAM6;
                break;
        }

        switch (t3.value)
        {
            case 0:
                list[0] = Teams.TEAM1;
                break;
            case 1:
                list[1] = Teams.TEAM2;
                break;
            case 2:
                list[2] = Teams.TEAM3;
                break;
            case 3:
                list[3] = Teams.TEAM4;
                break;
            case 4:
                list[4] = Teams.TEAM5;
                break;
            case 5:
                list[5] = Teams.TEAM6;
                break;
        }

        switch (t4.value)
        {
            case 0:
                list[0] = Teams.TEAM1;
                break;
            case 1:
                list[1] = Teams.TEAM2;
                break;
            case 2:
                list[2] = Teams.TEAM3;
                break;
            case 3:
                list[3] = Teams.TEAM4;
                break;
            case 4:
                list[4] = Teams.TEAM5;
                break;
            case 5:
                list[5] = Teams.TEAM6;
                break;
        }

        switch (t5.value)
        {
            case 0:
                list[0] = Teams.TEAM1;
                break;
            case 1:
                list[1] = Teams.TEAM2;
                break;
            case 2:
                list[2] = Teams.TEAM3;
                break;
            case 3:
                list[3] = Teams.TEAM4;
                break;
            case 4:
                list[4] = Teams.TEAM5;
                break;
            case 5:
                list[5] = Teams.TEAM6;
                break;
        }

        switch (t6.value)
        {
            case 0:
                list[0] = Teams.TEAM1;
                break;
            case 1:
                list[1] = Teams.TEAM2;
                break;
            case 2:
                list[2] = Teams.TEAM3;
                break;
            case 3:
                list[3] = Teams.TEAM4;
                break;
            case 4:
                list[4] = Teams.TEAM5;
                break;
            case 5:
                list[5] = Teams.TEAM6;
                break;
        }
        return list;
    }
    public Status[] SwitchStatus(TMPro.TMP_Dropdown s2, TMPro.TMP_Dropdown s3, TMPro.TMP_Dropdown s4, TMPro.TMP_Dropdown s5, TMPro.TMP_Dropdown s6)
    {
        Status[] list = new Status[2];

        switch (s2.value)
        {
            case 0:
                list[0] = Status.Open;
                break;
            case 1:
                list[1] = Status.Closed;
                break;
            case 2:
                list[2] = Status.AIMode;
                break;
        }
        switch (s3.value)
        {
            case 0:
                list[0] = Status.Open;
                break;
            case 1:
                list[1] = Status.Closed;
                break;
            case 2:
                list[2] = Status.AIMode;
                break;
        }
        switch (s4.value)
        {
            case 0:
                list[0] = Status.Open;
                break;
            case 1:
                list[1] = Status.Closed;
                break;
            case 2:
                list[2] = Status.AIMode;
                break;
        }
        switch (s5.value)
        {
            case 0:
                list[0] = Status.Open;
                break;
            case 1:
                list[1] = Status.Closed;
                break;
            case 2:
                list[2] = Status.AIMode;
                break;
        }
        switch (s6.value)
        {
            case 0:
                list[0] = Status.Open;
                break;
            case 1:
                list[1] = Status.Closed;
                break;
            case 2:
                list[2] = Status.AIMode;
                break;
        }
        return list;

    }
    public PlayerColor[] SwitchColor(TMPro.TMP_Dropdown c1, TMPro.TMP_Dropdown c2, TMPro.TMP_Dropdown c3, TMPro.TMP_Dropdown c4, TMPro.TMP_Dropdown c5, TMPro.TMP_Dropdown c6)
    {
        PlayerColor[] list = new PlayerColor[5];
        switch (c1.value)
        {
            case 0:
                list[0] = PlayerColor.RED;
                break;
            case 1:
                list[1] = PlayerColor.BLUE;
                break;
            case 2:
                list[2] = PlayerColor.GREEN;
                break;
            case 3:
                list[3] = PlayerColor.YELLOW;
                break;
            case 4:
                list[4] = PlayerColor.PURPLE;
                break;
            case 5:
                list[5] = PlayerColor.ORANGE;
                break;
        }

        switch (c2.value)
        {
            case 0:
                list[0] = PlayerColor.RED;
                break;
            case 1:
                list[1] = PlayerColor.BLUE;
                break;
            case 2:
                list[2] = PlayerColor.GREEN;
                break;
            case 3:
                list[3] = PlayerColor.YELLOW;
                break;
            case 4:
                list[4] = PlayerColor.PURPLE;
                break;
            case 5:
                list[5] = PlayerColor.ORANGE;
                break;
        }

        switch (c3.value)
        {
            case 0:
                list[0] = PlayerColor.RED;
                break;
            case 1:
                list[1] = PlayerColor.BLUE;
                break;
            case 2:
                list[2] = PlayerColor.GREEN;
                break;
            case 3:
                list[3] = PlayerColor.YELLOW;
                break;
            case 4:
                list[4] = PlayerColor.PURPLE;
                break;
            case 5:
                list[5] = PlayerColor.ORANGE;
                break;
        }

        switch (c4.value)
        {
            case 0:
                list[0] = PlayerColor.RED;
                break;
            case 1:
                list[1] = PlayerColor.BLUE;
                break;
            case 2:
                list[2] = PlayerColor.GREEN;
                break;
            case 3:
                list[3] = PlayerColor.YELLOW;
                break;
            case 4:
                list[4] = PlayerColor.PURPLE;
                break;
            case 5:
                list[5] = PlayerColor.ORANGE;
                break;
        }

        switch (c5.value)
        {
            case 0:
                list[0] = PlayerColor.RED;
                break;
            case 1:
                list[1] = PlayerColor.BLUE;
                break;
            case 2:
                list[2] = PlayerColor.GREEN;
                break;
            case 3:
                list[3] = PlayerColor.YELLOW;
                break;
            case 4:
                list[4] = PlayerColor.PURPLE;
                break;
            case 5:
                list[5] = PlayerColor.ORANGE;
                break;
        }

        switch (c6.value)
        {
            case 0:
                list[0] = PlayerColor.RED;
                break;
            case 1:
                list[1] = PlayerColor.BLUE;
                break;
            case 2:
                list[2] = PlayerColor.GREEN;
                break;
            case 3:
                list[3] = PlayerColor.YELLOW;
                break;
            case 4:
                list[4] = PlayerColor.PURPLE;
                break;
            case 5:
                list[5] = PlayerColor.ORANGE;
                break;
        }
        return list;

    }
    /// <summary>
    /// Save Team 1 Data with data in the parameters.
    /// </summary>
    public void SavePlayer1Data(Status PAI,Teams teamNumber , God god, PlayerColor color)
     {
        Player1.PAI = PAI;
        Player1.teamNumber = teamNumber;
        Player1.god = god;
        Player1.playerColor = color;
     }

    /// <summary>
    /// Save Team 2 Data with data in the parameters.
    /// </summary>
    public void SavePlayer2Data(Status PAI, Teams teamNumber, God god, PlayerColor color)
    {
        Player2.PAI = PAI;
        Player2.teamNumber = teamNumber;
        Player2.god = god;
        Player2.playerColor = color;

    }     /// <summary>
          /// Save Team 3 Data with data in the parameters.
          /// </summary>
    public void SavePlayer3Data(Status PAI, Teams teamNumber, God god, PlayerColor color)
    {
        Player3.PAI = PAI;
        Player3.teamNumber = teamNumber;
        Player3.god = god;
        Player3.playerColor = color;

    }     /// <summary>
          /// Save Team 4 Data with data in the parameters.
          /// </summary>
    public void SavePlayer4Data(Status PAI, Teams teamNumber, God god, PlayerColor color)
    {
        Player4.PAI = PAI;
        Player4.teamNumber = teamNumber;
        Player4.god = god;
        Player4.playerColor = color;

    }     /// <summary>
          /// Save Team 5 Data with data in the parameters.
          /// </summary>
    public void SavePlayer5Data(Status PAI, Teams teamNumber, God god, PlayerColor color)
    {
        Player5.PAI = PAI;
        Player5.teamNumber = teamNumber;
        Player5.god = god;
        Player5.playerColor = color;

    }     /// <summary>
          /// Save Team 6 Data with data in the parameters.
          /// </summary>
    public void SavePlayer6Data(Status PAI, Teams teamNumber, God god, PlayerColor color)
    {
        Player6.PAI = PAI;
        Player6.teamNumber = teamNumber;
        Player6.god = god;
        Player6.playerColor = color;

    }

}
