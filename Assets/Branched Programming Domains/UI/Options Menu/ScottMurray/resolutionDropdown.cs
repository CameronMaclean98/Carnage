using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resolutionDropdown : MonoBehaviour
{
    public Dropdown resDropdown;

    private void Update()
    {
        switch(resDropdown.value)
        {
            case 0:
                Screen.SetResolution(640, 480, false);
                break;
            case 1:
                Screen.SetResolution(720, 576, false);
                break;
            case 2:
                Screen.SetResolution(800, 600, false);
                break;
            case 3:
                Screen.SetResolution(1024, 768, false);
                break;
            case 4:
                Screen.SetResolution(1152, 864, false);
                break;
            case 5:
                Screen.SetResolution(1280, 900, false);
                break;
            case 6:
                Screen.SetResolution(1600, 900, false);
                break;
            case 7:
                Screen.SetResolution(1920, 1080, false);
                break;
        }

    }
    void Start()
    {
    
    }

   
}
