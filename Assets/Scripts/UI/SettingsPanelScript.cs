using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelScript : BasePanel
{
   public void UpdateInfo()
    {
        LocalConfig.Configdata configdata = LocalConfig.LoadConfigData();
        Slider soundSlider = transform.Find("Settings/Sound/Slider").GetComponent<Slider>();
        Slider musicSlider = transform.Find("Settings/Music/Slider").GetComponent<Slider>();
        Toggle fullScreenToggle = transform.Find("Settings/FullScreen/Toggle").GetComponent<Toggle>();
        Dropdown fPSDropdown = transform.Find("Settings/FPS/Dropdown").GetComponent<Dropdown>();
        soundSlider.value=configdata.soundVolume;
        musicSlider.value=configdata.musicVolume;
        fullScreenToggle.isOn = configdata.isFullScreen;
        fPSDropdown.value = configdata.FPS;
    }
}
