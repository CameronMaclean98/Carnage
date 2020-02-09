using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelection : MonoBehaviour
{
    readonly List<string> Colors = new List<string>() { "Choose a Color", "Red", "Blue", "Green", "Yellow", "Purple", "Orange" };
    public TMPro.TMP_Dropdown ColorP;
    public TMPro.TMP_Text ColorName;
    public TeamData teamData;

    public void Dropdown(int index)
    {
        ColorName.text = Colors[index];
        if(index == 1)
        {
            teamData.playerColor = PlayerColor.RED;
        }
        if (index == 2)
        {
            teamData.playerColor = PlayerColor.BLUE;
        }
        if (index == 3)
        {
            teamData.playerColor = PlayerColor.GREEN;
        }
        if (index == 4)
        {
            teamData.playerColor = PlayerColor.YELLOW;
        }
        if (index == 5)
        {
            teamData.playerColor = PlayerColor.PURPLE;
        }
        if (index == 6)
        {
            teamData.playerColor = PlayerColor.ORANGE;
        }
        if (index == 0)
        {
            ColorName.text = "";
        }
    }
    void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        ColorP.AddOptions(Colors);
    }
}
