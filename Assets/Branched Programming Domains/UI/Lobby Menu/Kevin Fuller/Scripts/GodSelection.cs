using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GodSelection : MonoBehaviour
{
    List<string> Gods = new List<string>() { "Choose a God","Cebil the Tactician","Fermund the Evangelist", "Branno the Titan", "Mal'Bain the Mystic", "Theros the Spearhead", "Nima the Gizmech"};
    public TMPro.TMP_Dropdown PlayerGod;
    public TMPro.TMP_Text PlayerGodName; //For the image text
    public Image img;
    public Sprite Cebil, Fermund, Branno, MalBain, Theros, Nima;
    public TMPro.TMP_Text GodName; //for the dropdown
    public TeamData teamData;
    
    void Start()
    {
        PopulateList();
    }
    public void Dropdown(int index)
    {
        GodName.text = Gods[index];
        if(index == 1)
        { 
            teamData.god = God.Cebil;
            img.sprite = Cebil;
            Debug.Log(teamData.god);
        }
        else if(index == 2)
        {
            teamData.god = God.Fermund;
            img.sprite = Fermund;
            Debug.Log(teamData.god);
        }
        else if(index == 3)
        {
            teamData.god = God.Branno;
            img.sprite = Branno;

            Debug.Log(teamData.god);
        }
        else if (index == 4)
        {
            teamData.god = God.Malbain;
            img.sprite = MalBain;

            Debug.Log(teamData.god);
        }
        else if (index == 5)
        {
            teamData.god = God.Theros;
            img.sprite = Theros;

            Debug.Log(teamData.god);
        }
        else if (index == 6)
        {
            teamData.god = God.Nima;
            img.sprite = Nima;

            Debug.Log(teamData.god);
        }
        else if(index == 0)
        {
            GodName.text = "";
            img.sprite = null;
        }
    }

    void PopulateList()
    {
        PlayerGod.AddOptions(Gods);
    }
}
