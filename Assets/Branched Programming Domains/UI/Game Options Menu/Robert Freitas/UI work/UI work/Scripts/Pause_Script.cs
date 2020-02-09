using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Pause_Script : MonoBehaviour
{

    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;

    public static bool gameispaused = false;

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameispaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameispaused = false;

        if(OptionsMenuUI == true)
        {
            OptionsMenuUI.SetActive(false);
        }

    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);

        Time.timeScale = 0;
        gameispaused = true;
    }

    public void loadmenu()
    {
        //SceneManager.LoadScene("options2");
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(true);

    }

    public void loadpause()
    {
        PauseMenuUI.SetActive(true);
        OptionsMenuUI.SetActive(false);

    }

    public void quitgame()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
