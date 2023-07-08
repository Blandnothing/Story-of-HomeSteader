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
        set
        {
            instance = value;
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
    public PlayerInventoryManager()
    {
        this.itemList = new List<Item>();
        this.itemNumDic = new Dictionary<Item, int>();
    }
    public void ChangeItemNum(Item item,int count)
    {
        if (itemNumDic.ContainsKey(item))
        {
            itemNumDic[item] += count;
            if (itemNumDic[item]<=0)
            {
                itemNumDic.Remove(item);
                itemList.Remove(item);
            }
        }
            
        else itemNumDic.Add(item, count);
    }
}
