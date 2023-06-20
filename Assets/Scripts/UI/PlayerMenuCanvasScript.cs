using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuCanvasScript : BasePanel
{
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape))
            ChangeInventoryPanel();
    }
    public void ChangeInventoryPanel()
    {

        if (UIManager.Instance.panelDict.ContainsKey(UIConst.InventoryPanel))
            UIManager.Instance.panelDict[UIConst.InventoryPanel].SetActive(!UIManager.Instance.panelDict[UIConst.InventoryPanel].isActiveAndEnabled);
        else if (GameObject.Find("UI").transform.Find(UIConst.InventoryPanel))
        {
            UIManager.Instance.AddPanel(GameObject.Find("UI").transform.Find(UIConst.InventoryPanel).gameObject);
            UIManager.Instance.panelDict[UIConst.InventoryPanel].SetActive(!UIManager.Instance.panelDict[UIConst.InventoryPanel].isActiveAndEnabled);
        }
        else
            UIManager.Instance.OpenPanel(UIConst.InventoryPanel);
    }
    
}
