using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    public enum itemType
    {
        Material,
        Consumable,
        Equipment,
        KeyItem
    }
    public itemType type;
    public string itemName;
    public Sprite itemImage;
    [TextArea]
    public string itemInfo;
}
