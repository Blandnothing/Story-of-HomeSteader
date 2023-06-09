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
        {
            if(UIManager.Instance.panelDict[UIConst.InventoryPanel].isActiveAndEnabled)
                UIManager.Instance.panelDict[UIConst.InventoryPanel].SetActive(false);
            else
                UIManager.Instance.panelDict[UIConst.InventoryPanel].SetActive(true);
        }
        else
            UIManager.Instance.OpenPanel(UIConst.InventoryPanel);
    }
}
