using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void switchscenes(string sceneName)
    {
            SceneManager.LoadScene(sceneName);
        //may need to check if script functions outside editor
    }
}
