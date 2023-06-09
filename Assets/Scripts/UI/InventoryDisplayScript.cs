using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayScript : BasePanel
{
    static InventoryDisplayScript instance;
    public static InventoryDisplayScript Instance
    {
        get { return instance; }
    }
    [SerializeField] GameObject gridPrefab;
    [SerializeField] GameObject m_Inventory;
    override protected void Awake()
    {
        if (instance!=null)
          Destroy(this); 
        instance = this;
    }
    private void OnEnable()
    {
           UpdateItemToUI();
    }
    public void InsertItemToUI(Item item)
    {
        InventoryGridScript gridInventory=Instantiate(instance.gridPrefab,instance.m_Inventory.transform).GetComponent<InventoryGridScript>();
        gridInventory.GridImage.sprite = item.itemImage;
        gridInventory.GridNum.text = PlayerInventoryManager.Instance.ItemNumDic[item].ToString();
        gridInventory.gridItem=item;
    }
    public void UpdateItemToUI()
    {
        for (int i = m_Inventory.transform.childCount-1; i >= 0 ; i--)
        {
            Destroy(m_Inventory.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < PlayerInventoryManager.Instance.itemList.Count; i++)
        {
            InsertItemToUI(PlayerInventoryManager.Instance.itemList[i]);
        }
    }

    
}
