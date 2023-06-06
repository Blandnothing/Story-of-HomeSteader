using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainConfigManagerScript : MonoBehaviour
{
    static LocalConfig.Configdata configData;
    static bool isCreated=false;
    [SerializeField] AudioMixer audioMixer;

    private void Awake()
    {
        if (isCreated)
        {
            Destroy(gameObject);
            return;
        }
        if (LocalConfig.LoadConfigData() == null)
            LocalConfig.SaveConfigData(new LocalConfig.Configdata(1, 1, 1, false));
        configData = LocalConfig.LoadConfigData();
        ChangeAll();
        isCreated = true;
    }
    public void ChangeSoundVolume(float soundVolume)
    {
        configData.soundVolume = soundVolume;
        audioMixer.SetFloat("SoundParam", soundVolume);
        LocalConfig.SaveConfigData(configData);
    }
    public void ChangeMusicVolume(float musicVolume)
    {
        configData.musicVolume = musicVolume;
        audioMixer.SetFloat("MusicParam", musicVolume);
        LocalConfig.SaveConfigData(configData);
    }

    public void ChangeFPS(int fps)
    {
        configData.FPS = fps;
        LocalConfig.SaveConfigData(configData);
        if (fps == 0)
            fps = 30;
        else if (fps == 1)
            fps = 60;
        else if (fps == 2)
            fps = 120;
        else fps = -1; 
        Application.targetFrameRate=fps;
        
    }
    public void ChangeFullScreen(bool isFullScreen)
    {
        configData.isFullScreen = isFullScreen;
        Screen.fullScreen=isFullScreen;
        LocalConfig.SaveConfigData(configData);
    }
    void ChangeAll()
    {
        ChangeSoundVolume(configData.soundVolume);
        ChangeMusicVolume(configData.musicVolume);
        ChangeFPS(configData.FPS);
        ChangeFullScreen(configData.isFullScreen);
    }
}
