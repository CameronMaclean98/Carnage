using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine;

public class TeamSelection : MonoBehaviour
{
    readonly List<string> PickTeam = new List<string>() { "Choose a Team", "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };
    public TMPro.TMP_Dropdown pickTeam;
    public TMPro.TMP_Text TeamName;
    public TeamData teamData;

    public void Dropdown(int index)
    {
        TeamName.text = PickTeam[index];
        if(index == 0)
        {
            TeamName.text = "";
        }
        else if (index == 1)
        {
            teamData.teamNumber = Teams.TEAM1;
        }
        else if (index == 2)
        {
            teamData.teamNumber = Teams.TEAM2;
        }
        else if (index == 3)
        {
            teamData.teamNumber = Teams.TEAM3;
        }
        else if (index == 4)
        {
            teamData.teamNumber = Teams.TEAM4;
        }
        else if (index == 5)
        {
            teamData.teamNumber = Teams.TEAM5;
        }
        else if (index == 6)
        {
            teamData.teamNumber = Teams.TEAM6;
        }
    }
    void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        pickTeam.AddOptions(PickTeam);
    }
}
