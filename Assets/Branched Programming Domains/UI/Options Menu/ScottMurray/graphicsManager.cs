using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class graphicsManager : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown textureDropdown;
    public Toggle fullScreenToggle;

    public Resolution[] resolutions;
    public graphicsSettings graphicsSettings;
    public Button applyButton;

    private void OnEnable()
    {
        graphicsSettings = new graphicsSettings();
        

        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureDropdown.onValueChanged.AddListener(delegate { OnTextureChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });
        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        loadSettings();
    }

    public void OnFullscreenToggle()
    {
        graphicsSettings.fullscreen = Screen.fullScreen = fullScreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        graphicsSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnTextureChange()
    {
        QualitySettings.masterTextureLimit = graphicsSettings.textureQuality = textureDropdown.value;
        
    }
    public void OnApplyButtonClick()
    {
        saveSettings();
    }
    public void saveSettings()
    {
        string jsonData = JsonUtility.ToJson(graphicsSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/graphicssettings.json", jsonData);
    }

    public void loadSettings()
    {
        graphicsSettings = JsonUtility.FromJson<graphicsSettings>(File.ReadAllText(Application.persistentDataPath + "/graphicssettings.json"));
        resolutionDropdown.value = graphicsSettings.resolutionIndex;
        textureDropdown.value = graphicsSettings.textureQuality;
        fullScreenToggle.isOn = graphicsSettings.fullscreen;
    }
}
