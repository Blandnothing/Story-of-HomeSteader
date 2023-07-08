using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static LocalPlayerData;

public class LoadAllData : MonoBehaviour
{
    public void LoadAll()
    {
        LocalPlayerData.PlayerData playerData= LocalPlayerData.LoadPlayerData();
        SceneManager.LoadScene(playerData.numScene);
        
        StopCoroutine(ChangePosition(playerData));
        StartCoroutine(ChangePosition(playerData));

        PlayerInventoryManager playerInventory= LocalPlayerInventoryDada.LoadInventoryData();
        PlayerInventoryManager.Instance = playerInventory;

        UIManager.Instance.Clear();
    }
    IEnumerator ChangePosition(LocalPlayerData.PlayerData playerData)
    {
        yield return new WaitForSeconds(1);
        GameObject player = GameObject.Find("HeroKnight");
        player.GetComponent<PlayerScript>().UpdateInfo(playerData.playerInfo);
        player.transform.position = new Vector2(playerData.playerTransformx, playerData.playerTransformy);
    }
}
