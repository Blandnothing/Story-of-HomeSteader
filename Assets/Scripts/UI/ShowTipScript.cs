using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTipScript : MonoBehaviour
{
    [SerializeField] GameObject tipText;
    [SerializeField] Item coin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerScript>().isInteracted = false;
            tipText.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<PlayerScript>().isInteracted)
        {
            collision.GetComponent<PlayerScript>().isInteracted=false;
            Interact(collision.GetComponent<PlayerScript>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tipText.SetActive(false);
        }
    }
    public void Interact(PlayerScript player)
    {
        int coinNum;
        if (PlayerInventoryManager.Instance.ItemNumDic.TryGetValue(coin,out coinNum) && coinNum>=2)
        {
            PlayerInventoryManager.Instance.ChangeItemNum(coin, -2);
            player.attackPower += 2;
            MessageBoxScript.Instance.ChangeMessage("攻击力已增加");
        }
        else
        {
            MessageBoxScript.Instance.ChangeMessage("硬币不足");
        }
    }
}
