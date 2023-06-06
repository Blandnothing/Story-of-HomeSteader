using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class UIManager
{
    private static UIManager _instance;
    private Transform _uiRoot;
    //·�������ֵ�
    private Dictionary<string, string> pathDict;
    //Ԥ�Ƽ������ֵ�
    private Dictionary<string, GameObject> prefabDict;
    //�Ѵ򿪽���Ļ����ֵ�
    public Dictionary<string, BasePanel> panelDict;
    public static UIManager Instance { get { if (_instance == null) _instance = new UIManager(); return _instance; } }
    public Transform UIRoot
    {
        get
        {
            if(_uiRoot == null)
            {
                if (GameObject.Find("DontDestroyObject"))
                {
                    _uiRoot = GameObject.Find("DontDestroyObject").transform;
                }
                else
                {
                    _uiRoot = new GameObject("DontDestroyObject").transform;
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
        //�������Ƿ��Ѵ�
        if(panelDict.TryGetValue(name,out panel))
        {
            Debug.Log("�����Ѵ�" + name);
            return null;
        }

        string path = "";
        //���·���Ƿ�������
        if(!pathDict.TryGetValue(name,out path))
        {
            Debug.Log("�������ƴ���" + name);
            return null;
        }

        GameObject panelPrefab = null;
        //ʹ�û���Ԥ�Ƽ�
        if(!prefabDict.TryGetValue(name,out panelPrefab))
        {
            string realPath = "Prefabs/"+path;
            panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
            prefabDict.Add(name, panelPrefab);
        }

        //�򿪽���
        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, true);
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
            Debug.Log("����δ��");
            return false;
        }
        panel.ClosePanel();
        return true;
    }
}



 public class UIConst
 {
     public const string InventoryPanel = "InventoryPanel";
     public const string SettingsPanel = "SsttingsPanel";
 }

