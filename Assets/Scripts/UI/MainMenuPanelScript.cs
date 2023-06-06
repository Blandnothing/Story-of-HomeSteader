using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanelScript : BasePanel
{
    public void OpenSettingsPanel()
    {
        if(UIManager.Instance.panelDict.ContainsKey(UIConst.SettingsPanel))
            UIManager.Instance.panelDict[UIConst.SettingsPanel].SetActive(true);
        else
            UIManager.Instance.OpenPanel(UIConst.SettingsPanel);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
