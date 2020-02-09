using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneChange : MonoBehaviour
{
    public void changeToCredits()
    {
        SceneManager.LoadScene("End Credits"); // Hector's Credits Scene
    }

    public void changeToOptions()
    {
        SceneManager.LoadScene("optionMenu"); // Scott's Options Scene
    }

    public void changeToSinglePlayer()
    {
        SceneManager.LoadScene("kevinworkspace"); // Kevin's Single-Player Lobby Scene
    }

    public void changeToMultiPlayer()
    {
        SceneManager.LoadScene("kevinworkspace"); // Kevin's Multi-Player Lobby Scene
    }

    public void exitGame()
    {
        Application.Quit(); // Quit Game
    }
}
