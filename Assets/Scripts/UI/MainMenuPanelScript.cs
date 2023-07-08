using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanelScript : BasePanel
{
    [SerializeField] PlayerMenuCanvasScript playerMenu;
    [SerializeField] AudioClip musicMenu;
    void Start()
    {
        MusicManager.Instance.PlayMusic(musicMenu);
    }
    public void OpenSettingsPanel()
    {
        
        if (UIManager.Instance.panelDict.ContainsKey(UIConst.SettingsPanel))
            UIManager.Instance.panelDict[UIConst.SettingsPanel].SetActive(true);
        else if (GameObject.Find("UI").transform.Find(UIConst.SettingsPanel))
        {
            UIManager.Instance.AddPanel(GameObject.Find("UI").transform.Find(UIConst.SettingsPanel).gameObject);
            UIManager.Instance.panelDict[UIConst.SettingsPanel].SetActive(true);
        }
        else
            UIManager.Instance.OpenPanel(UIConst.SettingsPanel);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
        playerMenu.SetActive(true);
    }
}
