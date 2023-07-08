using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAllData : MonoBehaviour
{
    public void SaveAll()
    {
        GameObject player = GameObject.Find("HeroKnight");
        LocalPlayerData.SavePlayerData(new LocalPlayerData.PlayerData(player.GetComponent<PlayerScript>().GetPlayerInfo(), player.transform, player.scene.buildIndex));

        LocalPlayerInventoryDada.SaveInventoryData(PlayerInventoryManager.Instance);

        MessageBoxScript.Instance.ChangeMessage("�浵�ɹ�");
    }
}
