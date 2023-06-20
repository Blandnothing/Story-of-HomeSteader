using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class UIManager
{
    private static UIManager _instance;
    private Transform _uiRoot;
    //路径配置字典
    private Dictionary<string, string> pathDict;
    //预制件缓存字典
    private Dictionary<string, GameObject> prefabDict;
    //已打开界面的缓存字典
    public Dictionary<string, BasePanel> panelDict;
    public static UIManager Instance { get { if (_instance == null) _instance = new UIManager(); return _instance; } }
    public Transform UIRoot
    {
        get
        {
            if (_uiRoot == null)
            {
                if (GameObject.Find("StartDontDestroyObject"))
                {
                    _uiRoot = GameObject.Find("StartDontDestroyObject").transform;
                }
                else
                {
                    _uiRoot = new GameObject("StartDontDestroyObject").transform;
                }
            }
            return _uiRoot;
        }
    }
    private UIManager()
    {
        InitDicts();
    }

    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();

        pathDict = new Dictionary<string, string>()
        {
            {UIConst.InventoryPanel,"UI/Inventory Panel" },
            {UIConst.SettingsPanel,"UI/Settings Panel" }
        };   
    }
    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        //检查界面是否已打开
        if(panelDict.TryGetValue(name,out panel))
        {
            Debug.Log("界面已打开" + name);
            return null;
        }

        string path = "";
        //检查路径是否已配置
        if(!pathDict.TryGetValue(name,out path))
        {
            Debug.Log("界面名称错误" + name);
            return null;
        }

        GameObject panelPrefab;
        //使用缓存预制件
        if(!prefabDict.TryGetValue(name,out panelPrefab))
        {
            string realPath = "Prefabs/"+path;
            panelPrefab = Resources.Load<GameObject>(realPath);
            prefabDict.Add(name, panelPrefab);
        }

        //打开界面
        GameObject panelObject = GameObject.Instantiate(panelPrefab,UIRoot,false);
        panel = panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        panel.OpenPanel(name);
        return panel;
    }
    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        if(!panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("界面未打开");
            return false;
        }
        panel.ClosePanel();
        return true;
    }
    public void AddPanel(GameObject panel)
    {
        string path = "";
        //检查路径是否已配置
        if (!pathDict.TryGetValue(panel.name, out path))
        {
            Debug.Log("界面名称错误" + panel.name);
            return;
        }

        GameObject panelPrefab;
        //使用缓存预制件
        if (!prefabDict.TryGetValue(panel.name, out panelPrefab))
        {
            string realPath = "Prefabs/" + path;
            panelPrefab = Resources.Load<GameObject>(realPath);
            prefabDict.Add(panel.name, panelPrefab);
        }
        //添加界面
        panelDict.Add(panel.name, panel.GetComponent<BasePanel>());
        return;
    }
}



 public class UIConst
 {
     public const string InventoryPanel = "Inventory Panel";
     public const string SettingsPanel = "Settings Panel";
 }

