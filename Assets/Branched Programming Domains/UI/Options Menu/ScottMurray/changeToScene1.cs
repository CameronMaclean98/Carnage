using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeToScene1 : MonoBehaviour
{
    public void sceneChange1()
    {
        SceneManager.LoadScene("Scene1");
    }
}
