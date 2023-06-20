using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnSceneScript : MonoBehaviour
{
    public Item item;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (!PlayerInventoryManager.Instance.itemList.Contains(item))
            {
                PlayerInventoryManager.Instance.itemList.Add(item);
            }
            PlayerInventoryManager.Instance.ChangeItemNum(item,1);
            if(UIManager.Instance.panelDict.ContainsKey(UIConst.SettingsPanel))
                InventoryDisplayScript.Instance.UpdateItemToUI();
            MessageBoxScript.Instance.ChangeMessage(item.itemName + "+1");
            Destroy(this.gameObject);
        }
    }
}
