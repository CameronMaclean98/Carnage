using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OpenClosedAI : MonoBehaviour
{
    public GameObject God;
    public GameObject GodImage;
    public GameObject Team;
    public GameObject Color;
    List<string> status = new List<string>() { "Open", "Closed", "AI Mode" };
    public TMPro.TMP_Dropdown JoinStatus;
    public TMPro.TMP_Text Status;
    public TMPro.TMP_Text PlayerName;
    public TeamData teamData;

    
    public void DropdownChange(int index)
    {
        Status.text = status[index]; //fills the dropdown with the three statuses
        //by default, the status should be open
        PlayerName.text = teamData.name;
        if (index == 0)
        {
            God.SetActive(true);
            GodImage.SetActive(true);
            Team.SetActive(true);
            Color.SetActive(true);
        }
        else if(index == 1)
        {
            God.SetActive(false);
            GodImage.SetActive(false);
            Team.SetActive(false);
            Color.SetActive(false);
            PlayerName.text = "";
        }
        else if(index == 2)
        {
            God.SetActive(false);
            GodImage.SetActive(false);
            Team.SetActive(false);
            Color.SetActive(false);
            PlayerName.text = "AI Mode";
        }
    }
    void Start()
    {
        PopulateList();
    }
    void PopulateList()
    {
        JoinStatus.AddOptions(status);
    }
}
