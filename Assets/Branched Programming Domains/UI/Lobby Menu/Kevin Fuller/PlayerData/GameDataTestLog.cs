using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataTestLog : MonoBehaviour
{
    public TeamData teamData;
    public TeamData Player1;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player 1 Data Status" + Player1.PAI);
        Debug.Log("Player 1 Team" + Player1.teamNumber);
        Debug.Log("Player 1 Color" + Player1.playerColor);
        Debug.Log("Player 1 God" + Player1.god);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
