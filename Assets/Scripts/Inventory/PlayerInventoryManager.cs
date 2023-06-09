using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager
{
    
    private static PlayerInventoryManager instance;
    public static PlayerInventoryManager Instance
    {
        get {
            instance ??= LocalPlayerInventoryDada.LoadInventoryData();
            instance ??= new PlayerInventoryManager(new List<Item>(),new Dictionary<Item, int>());
            return instance; 
        }
    }
   public List<Item> itemList;
   Dictionary<Item, int> itemNumDic;
    public Dictionary<Item, int> ItemNumDic { get { return itemNumDic; } }
   public PlayerInventoryManager(List<Item> itemList, Dictionary<Item, int> itemNumDic)
    {
        this.itemList = itemList;
        this.itemNumDic = itemNumDic;
    }
    public void ChangeItemNum(Item item,int count)
    {
        if(itemNumDic.ContainsKey(item))
            itemNumDic[item]+=count;
        else itemNumDic.Add(item, count);
    }
}
