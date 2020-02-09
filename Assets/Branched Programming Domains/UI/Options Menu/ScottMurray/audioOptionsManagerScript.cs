using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class audioOptionsManagerScript : MonoBehaviour
{
    public Slider gameVolumeSlider;
    public Slider musicVolumeSlider;

    public Slider soundEffectsVolumeSlider;
    public Slider diologueVolumeSlider;
    public Slider uiVolumeSlider;
    public audioOptionsSettings audioOptionsSettings;

    public void OnEnable()
    {
        audioOptionsSettings = new audioOptionsSettings();
        
        gameVolumeSlider.onValueChanged.AddListener(delegate { OnGameVolumeChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        soundEffectsVolumeSlider.onValueChanged.AddListener(delegate { OnSoundEffectsVolumeChange(); });
        diologueVolumeSlider.onValueChanged.AddListener(delegate { OnDiologueVolumeChange(); });
        //uiVolumeSlider.onValueChanged.AddListener(delegate { OnUIVolumeChange(); });
        
    }
    public void OnGameVolumeChange()
    {
      // = gameVolumeSlider.Value;  
    }
    public void OnMusicVolumeChange()
    {
        // = musicVolumeSlider.Value;
    }
    public void OnSoundEffectsVolumeChange()
    {
        // = soundEffectsVolumeSlider.Value;
    }
    public void OnDiologueVolumeChange()
    {
        // = diologueVolumeSlider.Value;
    }
    public void OnUIVolumeChange()
    {
        // = uiVolumeSlider.Value;
    }

    public Slider soundEffectsSlider;
    //public Slider diologueVolumeSlider;
    //public Slider uiVolumeSlider;

}
