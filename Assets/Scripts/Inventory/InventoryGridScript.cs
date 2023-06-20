using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridScript : MonoBehaviour
{
    public Item gridItem;
    [SerializeField] Image _gridImage;
    public Image GridImage { get {
            return _gridImage;
        } }
    [SerializeField] Text _gridNum;
    public Text GridNum { get { return _gridNum; } }
    [SerializeField] Text infoText;
    private void Awake()
    {
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
    }
    public void DisplayItemInfo()
    {
        infoText.text = gridItem.itemInfo;
    }
}
